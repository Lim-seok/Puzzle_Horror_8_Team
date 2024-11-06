using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.5f;
    private AudioSource bgmPlayer;
    private bool isBGMPlay;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.5f;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    public enum Sfx
    {
        Button,
        Beat,
        Hit
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

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
        bgmPlayer.Play();
    }

    public void PlayBGM(AudioClip newBgmClip = null)
    {
        if (newBgmClip != null)
        {
            bgmPlayer.clip = newBgmClip;
            bgmPlayer.Play();
        }
        else if (!bgmPlayer.isPlaying)
        {
            bgmPlayer.clip = bgmClip;
            bgmPlayer.Play();
        }
    }

    public void PlaySFX(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].PlayOneShot(sfxPlayers[loopIndex].clip);
            break;
        }
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
