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
        // sfxPlayers 배열을 순회
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            // 현재 채널 인덱스부터 시작하여 순환적으로 인덱스 계산
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;
            // 현재 오디오 플레이어가 재생 중이면 다음 플레이어로 넘어감
            if (sfxPlayers[loopIndex].isPlaying)
                continue;
            // 사용 가능한 오디오 플레이어를 찾으면 해당 SFX 클립을 설정하고 재생
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].PlayOneShot(sfxPlayers[loopIndex].clip);
            break; // 재생을 시작했으므로 루프 종료
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
