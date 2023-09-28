using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public uint bulletCount;
    public Transform PlayerPos;
    public Transform CameraPos;
    public List<LifeController> Enemies = new List<LifeController>();
    public Sprite locationImage;
    public int levelNum;

    public void Start()
    {
        foreach(LifeController life in Enemies)
        {
            life.OnDeath += CheckEndLevel;
        }
    }

    public void StartLocation()
    {
        MainGameController.main.DeactivateAllLevels();
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
    }

    private void CheckEndLevel()
    {
        foreach(LifeController enemy in Enemies)
        {
            if (enemy.gameObject.activeSelf)
                return;
        }

        HudUI.main.LevelSuccess();
    }
}
