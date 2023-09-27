using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public static HudUI main;

    [SerializeField] PullObjects _bulletsPull;
    [SerializeField] GameObject _exitPanel;
    [SerializeField] GameObject _successPanel;

    private Location _location;
    private WeaponModel _weapon;

    public void Awake()
    {
        main = this;
    }

    public void OnEnable()
    {

        if (_weapon != null)
            _weapon.OnChangeBullet += SetBullet;
    }

    public void OnDisable()
    {

        if (_weapon != null)
            _weapon.OnChangeBullet -= SetBullet;
    }

    public void Init(WeaponModel weapon, Location location)
    {
        _location = location;

        _exitPanel.SetActive(false);
        _successPanel.SetActive(false);

        if (_weapon == null)
        {
            _weapon = weapon;
            weapon.OnChangeBullet += SetBullet;
        }

        SetBullet();
    }

    private void SetBullet()
    {
        _bulletsPull.Clear();
        for (int i = 0; i < _weapon.BulletInClip; i++)
        {
            _bulletsPull.AddObj();
        }
    }

    public void ExitMenu()
    {
        _exitPanel.SetActive(true);

        _successPanel.SetActive(false);
    }

    public void NoExit()
    {
        _exitPanel.SetActive(false);

        _successPanel.SetActive(false);
    }

    public void YesExit()
    {
        _exitPanel.SetActive(false);

        _successPanel.SetActive(false);
        NewUIController.main.ExitMenu();
    }

    public void Restart()
    {
        _location.StartLocation();
    }

    public void LevelSuccess()
    {
        _exitPanel.SetActive(false);
        _successPanel.SetActive(true);
    }

    public void NextLevel()
    {
        MainGameController.main.NextLevel(_location);
    }
}
