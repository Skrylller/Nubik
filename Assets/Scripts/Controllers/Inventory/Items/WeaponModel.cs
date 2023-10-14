using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Items/Weapon")]
public class WeaponModel : ItemModel
{
    public enum WeaponType
    {
        None,
        Pistol,
        Rifle
    }

    [Header("Weapon")]
    [SerializeField] private BulletModel _bullet;
    [SerializeField] private WeaponType _weapon;
    [SerializeField] private uint _bulletInClipMax;
    [SerializeField] private float _shootDelay;
    [SerializeField] private uint _bulletÑountToShoot;
    [Range(0,360)]
    [SerializeField] private float _spreadAngle;
    [SerializeField] private SoundController.AudioObj _shootSound;

    private uint _bulletInClip;

    public uint BulletInClip 
    {
        set { 
            _bulletInClip = value;
            OnChangeBullet?.Invoke();
        }
        get { return _bulletInClip; }
    }

    public Action OnChangeBullet;

    public BulletModel Bullet { get { return _bullet; } }
    public WeaponType Weapon { get { return _weapon; } }
    public uint BullietInClipMax { get { return _bulletInClipMax; } }
    public float ShootDelay { get { return _shootDelay; } }
    public uint BulletÑount { get { return _bulletÑountToShoot; } }
    public float SpreadAngle { get { return _spreadAngle; } }
    public SoundController.AudioObj ShootSound => _shootSound;
}
