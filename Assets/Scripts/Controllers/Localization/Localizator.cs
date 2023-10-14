using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YG;

public class Localizator : MonoBehaviour
{
    public static Localizator main;

    public enum Launguage
    {
        English,
        Russia
    }

    [SerializeField] private Launguage _selectedLaunguage;

    public Launguage SelectedLaunguage => _selectedLaunguage;

    public Action<Launguage> OnChangeLaunguage;

    public void Awake()
    {
        main = this;
    }

    public void YandexLoadUpdateData()
    {
        ChangeLaunguage(YandexGame.savesData.myLanguage);
    }

    public void ChangeLaunguage(Launguage launguage)
    {
        if (_selectedLaunguage == launguage)
            return;

        _selectedLaunguage = launguage;
        YandexGame.savesData.myLanguage = (int)launguage;

        YandexGame.SaveProgress();
        OnChangeLaunguage?.Invoke(_selectedLaunguage);
    }

    public void ChangeLaunguage(int i)
    {
        ChangeLaunguage((Launguage)i);
    }
}
