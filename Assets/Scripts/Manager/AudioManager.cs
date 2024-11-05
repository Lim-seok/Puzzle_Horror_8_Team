using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.5f;
    AudioSource bgmPlayer;
    private bool isBGMPlay;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.5f;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx 
    {
        Button, 
        FlickerLightSound 
    }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
        bgmPlayer.Play();
    }

    public void PlaySFX(Sfx sfx)
    {
        sfxPlayers[(int)sfx].clip = sfxClips[(int)sfx];
        sfxPlayers[(int)sfx].PlayOneShot(sfxPlayers[(int)sfx].clip);
    }
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;

        if (bgmPlayer != null)
        {
            bgmPlayer.volume = bgmVolume;

            if (bgmVolume <= 0)
            {
                if (bgmPlayer.isPlaying)
                {
                    bgmPlayer.Pause();
                }
            }
            else
            {
                if (!bgmPlayer.isPlaying)
                {
                    bgmPlayer.Play();
                }
            }
        }
    }


    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        foreach (var player in sfxPlayers)
        {
            player.volume = sfxVolume;
        }
    }
}
