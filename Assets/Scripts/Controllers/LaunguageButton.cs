using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunguageButton : MonoBehaviour
{
    public enum State
    {
        select,
        deselect
    }

    [SerializeField] private Localizator.Launguage _launguage;
    [SerializeField] private ModeSwitcher _modeSwitcher;

    public void Start()
    {
        UpdateButton(Localizator.Launguage.English);
        Localizator.main.OnChangeLaunguage += UpdateButton;
    }

    public void UpdateButton(Localizator.Launguage laung)
    {
        if (Localizator.main.SelectedLaunguage == _launguage)
            _modeSwitcher.State = (int)State.select;
        else
            _modeSwitcher.State = (int)State.deselect;
    }
}
