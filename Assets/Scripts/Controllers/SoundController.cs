using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Plugins.Audio.Core;

public class SoundController : MonoBehaviour
{
    public static SoundController main;

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private List<AudioSource> SoundObjs = new List<AudioSource>();
    [SerializeField] private List<SourceAudio> SoundObjs2 = new List<SourceAudio>();
    [SerializeField] private List<AudioSource> MusicsObjs = new List<AudioSource>();
    [SerializeField] private List<SourceAudio> MusicsObjs2 = new List<SourceAudio>();

    [System.Serializable]
    public class AudioObj
    {
        public AudioClip clip;
        public string _key;
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

    private void PlaySound(AudioObj audio, int objNum)
    {
        if (audio.clip == null) 
            return;
        SoundObjs[objNum].volume = audio.volume;
        SoundObjs[objNum].pitch = UnityEngine.Random.Range(audio.pitch.x, audio.pitch.y);

        if (audio._key == "" || audio._key == null)
            SoundObjs[objNum].PlayOneShot(audio.clip);
        else
            SoundObjs2[objNum].Play(audio._key);
    }

    private int FindFreeSound(List<AudioSource> objs)
    {
        for(int i = 0; i < objs.Count; i++)
        {
            if (!objs[i].isPlaying)
            {
                return i;
            }
        }
        return 0;
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


        MusicsObjs[numMusic].gameObject.SetActive(true);

        if (audio._key == null || audio._key == "")
            MusicsObjs[numMusic].clip = audio.clip;
        else
        {
            MusicsObjs[numMusic].clip = audio.clip;
            MusicsObjs2[numMusic].Play(audio._key);
        }
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
