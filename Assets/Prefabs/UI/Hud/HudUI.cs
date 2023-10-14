using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public static HudUI main;

    [SerializeField] ItemModel.ItemType diamond;
    [SerializeField] PullObjects _bulletsPull;
    [SerializeField] GameObject _exitPanel;
    [SerializeField] GameObject _successPanel;
    [SerializeField] GameObject _restartPanel;
    [SerializeField] GameObject _losePanel;

    [SerializeField] List<GameObject> _stars = new List<GameObject>();

    [SerializeField] SoundController.AudioObj _successSound;
    [SerializeField] SoundController.AudioObj _loseSound;

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

        _successPanel.SetActive(false);
        _exitPanel.SetActive(false);
        _successPanel.SetActive(false);
        _losePanel.SetActive(false);
        _restartPanel.SetActive(false);

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

    public void RestartMenu()
    {
        _successPanel.SetActive(false);
        _restartPanel.SetActive(true);
        _exitPanel.SetActive(false);
        _losePanel.SetActive(false);
    }
    
    public void ExitMenu()
    {

        _restartPanel.SetActive(false);
        _exitPanel.SetActive(true);
        _successPanel.SetActive(false);
        _losePanel.SetActive(false);
    }

    public void NoExit()
    {
        _restartPanel.SetActive(false);
        _exitPanel.SetActive(false);
        _successPanel.SetActive(false);
    }

    public void YesExit()
    {
        NoExit();
        NewUIController.main.ExitMenu();
    }

    public void Restart()
    {
        _exitPanel.SetActive(false);
        _successPanel.SetActive(false);
        _restartPanel.SetActive(false);
        _losePanel.SetActive(false);
        _location.StartLocation();
    }

    public void LevelSuccess(int star)
    {
        SoundController.main.PlaySound(_successSound);

        for (int i = 0; i < _stars.Count; i++)
        {
            _stars[i].SetActive(false);
        }

        _successPanel.SetActive(false);
        _exitPanel.SetActive(false);
        _successPanel.SetActive(true);
        _losePanel.SetActive(false);

        for(int i = 0; i < star; i++)
        {
            _stars[i].SetActive(true);
        }

        MainGameController.main.SuccessLevel(_location, star);
    }

    public void Lose()
    {
        SoundController.main.PlaySound(_loseSound);
        _exitPanel.SetActive(false);
        _successPanel.SetActive(false);
        _restartPanel.SetActive(false);
        _losePanel.SetActive(true);
    }

    public void NextLevel()
    {
        MainGameController.main.NextLevel(_location);
    }

    public void PlusBullet()
    {
        if (PlayerInventory.Inventory.CheckItem(diamond, 5, true))
        {
            _weapon.BulletInClip++;
            _exitPanel.SetActive(false);
            _successPanel.SetActive(false);
            _restartPanel.SetActive(false);
            _losePanel.SetActive(false);
        }
    }
}
