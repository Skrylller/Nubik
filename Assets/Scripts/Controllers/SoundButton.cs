using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public enum VolumeStates
    {
        On,
        Off,
    }

    [SerializeField] ModeSwitcher _modeSwitcher;
    private SoundController _soundController;

    public void Start()
    {
        _soundController = SoundController.main;
        _soundController.OnMixerOff += SetState;
        _soundController.OnMixerOn += SetState;
        SetState();
    }

    public void OnEnable()
    {
        if (_soundController == null)
            return;

        _soundController.OnMixerOff += SetState;
        _soundController.OnMixerOn += SetState;
    }

    public void OnDisable()
    {
        if (_soundController == null)
            return;

        _soundController.OnMixerOff -= SetState;
        _soundController.OnMixerOn -= SetState;
    }

    public void SetState()
    {
        if (_soundController.Volume)
        {
            _modeSwitcher.State = (int)VolumeStates.On;
        }
        else
        {
            _modeSwitcher.State = (int)VolumeStates.Off;
        }
    }

    public void ChangeVolume()
    {

        if (_soundController.Volume)
        {
            SoundController.main.VolumeOff();
        }
        else
        {
            SoundController.main.VolumeOn();
        }
    }
}
