using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public uint bulletCount;
    public uint bulletCountStar;
    public Transform PlayerPos;
    public Transform CameraPos;
    public List<LifeController> Enemies = new List<LifeController>();
    public List<LifeController> Diamonds = new List<LifeController>();
    public Sprite locationImage;
    public int levelNum;

    public void Start()
    {
        foreach(LifeController life in Enemies)
        {
            life.OnDeath += EndLevel;
        }
    }

    public void StartLocation()
    {
        MainGameController.main.DeactivateAllLevels();
        MainGameController.main.SetLevel(this);
        gameObject.SetActive(true);
        PullsController.main.ClearAll();
        ShootingController.main._weapon.BulletInClip = bulletCount;
        Bootastrap.main.Player.transform.position = PlayerPos.position;
        Bootastrap.main.CameraTarget.SetTarget(CameraPos);
        Bootastrap.main.CameraTarget.Teleport();
        HudUI.main.Init(ShootingController.main._weapon, this);
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Diamonds.Count; i++)
        {
            Diamonds[i].gameObject.SetActive(true);
        }
    }

    public void EndLevel()
    {
        CheckEndLevel();
    }

    public bool CheckEndLevel()
    {
        foreach(LifeController enemy in Enemies)
        {
            if (enemy.gameObject.activeSelf)
                return false;
        }

        int star = 2;

        foreach (LifeController diamond in Diamonds)
        {
            if (diamond.gameObject.activeSelf)
               star = 1;
        }

        if (bulletCount - bulletCountStar <= MainGameController.main.weapon.BulletInClip)
            star++;

        HudUI.main.LevelSuccess(star);

        return true;
    }
}
