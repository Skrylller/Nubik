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
    public string levelName;

    public void StartLocation()
    {
        ShootingController.main._weapon.BulletInClip = bulletCount;
        Bootastrap.main.Player.transform.position = PlayerPos.position;
        Bootastrap.main.CameraTarget.SetTarget(CameraPos);
        Bootastrap.main.CameraTarget.Teleport();
        HudUI.main.Init(ShootingController.main._weapon);
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].gameObject.SetActive(true);
        }
    }
}
