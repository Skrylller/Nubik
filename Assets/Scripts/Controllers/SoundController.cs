using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public static SoundController main;

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private List<AudioSource> SoundObjs = new List<AudioSource>();
    [SerializeField] private List<AudioSource> MusicsObjs = new List<AudioSource>();

    [System.Serializable]
    public class AudioObj
    {
        public AudioClip clip;
        public Vector2 pitch = new Vector2(0.9f, 1.1f);
        public float volume = 1;
    }

    [SerializeField] private AudioObj _pressButtonSound;
    [SerializeField] private AudioObj _defaultMusic;

    private int numMusic = 0;

    public bool Volume;

    public Action OnMixerOff;
    public Action OnMixerOn;

    private void Awake()
    {
        main = this;
        Volume = true;
    }

    private void Start()
    {
        SetDefaultMusic();
    }

    private void Update()
    {
        MusicMuting();
    }

    public void PlaySound(AudioObj audio)
    {
        PlaySound(audio, FindFreeSound(SoundObjs));
    }

    private void PlaySound(AudioObj audio, AudioSource obj)
    {
        if (audio.clip == null) 
            return;

        obj.volume = audio.volume;
        obj.pitch = UnityEngine.Random.Range(audio.pitch.x, audio.pitch.y);
        obj.PlayOneShot(audio.clip);
    }

    private AudioSource FindFreeSound(List<AudioSource> objs)
    {
        foreach(AudioSource obj in objs)
        {
            if (!obj.isPlaying)
            {
                return obj;
            }
        }
        return objs[0];
    }

    public void PressButtonSound()
    {
        PlaySound(_pressButtonSound);
    }

    public void SetNewMusic(AudioObj audio)
    {
        numMusic = FindFreeMusic(MusicsObjs, audio.clip);
        MusicsObjs[numMusic].volume = audio.volume;
        MusicsObjs[numMusic].pitch = audio.pitch.x;
        MusicsObjs[numMusic].clip = audio.clip;
        MusicsObjs[numMusic].gameObject.SetActive(true);
    }
    public void SetDefaultMusic()
    {
        SetNewMusic(_defaultMusic);
    }

    public void VolumeOff()
    {
        Volume = false;
        MixerOff();
    }

    public void VolumeOn()
    {
        Volume = true;
        MixerOn();
    }

    public void MixerOff()
    {
        _mixer.SetFloat("Master", -80);
        OnMixerOff?.Invoke();
    }

    public void MixerOn()
    {
        if (!Volume)
            return;

        _mixer.SetFloat("Master", 20);
        OnMixerOn?.Invoke();
    }

    private int FindFreeMusic(List<AudioSource> objs, AudioClip clip)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].clip == clip && objs[i].gameObject.activeSelf)
            {
                return i;
            }
        }

        for (int i = 0; i < objs.Count; i++)
        {
            if (!objs[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return 0;
    }

    private void MusicMuting()
    {
        for (int i = 0; i < MusicsObjs.Count; i++)
        {
            if (i == numMusic)
                continue;

            MusicsObjs[i].volume = Mathf.Lerp(MusicsObjs[i].volume, 0, 0.02f);

            if (MusicsObjs[i].volume < 0.05f)
                MusicsObjs[i].gameObject.SetActive(false);
        }
    }
}
