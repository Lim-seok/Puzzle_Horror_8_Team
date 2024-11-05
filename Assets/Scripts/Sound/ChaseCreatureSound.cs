using UnityEngine;

public class ChaseCreatureSound : MonoBehaviour
{
    public ChaseCreature chaseCreature;
    public AudioSource bgmAudioSource;
    public AudioSource footstepAudioSource;
    public AudioSource jumpScareAudioSource;

    [Header("Audio Clips")]
    public AudioClip BGMClip;
    public AudioClip[] moveFootstepClip;
    public AudioClip[] chaseFootstepClip;
    public AudioClip[] jumpScareSoundClip;

    private bool hasDetectedPlayer = false;
    private bool isPlayingMoveFootstep = false;
    private bool isPlayingChaseFootstep = false;

    private void Update()
    {
        if (chaseCreature.aiState == AIState.Chasing || chaseCreature.aiState == AIState.Attacking)
        {
            if (!hasDetectedPlayer)
            {
                PlayDetectionBGM();
                hasDetectedPlayer = true;
            }

            if (chaseCreature.aiState == AIState.Chasing)
            {
                PlayChaseFootstep(0);
            }
            else if (chaseCreature.aiState == AIState.Wandering || chaseCreature.aiState == AIState.Idle)
            {
                PlayMoveFootstep(0); 
            }
        }
    }

    private void PlayDetectionBGM()
    {
        bgmAudioSource.clip = BGMClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    private void PlayMoveFootstep(int clipIndex)
    {
        if (isPlayingMoveFootstep || moveFootstepClip.Length <= clipIndex) return;

        isPlayingMoveFootstep = true;
        footstepAudioSource.clip = moveFootstepClip[clipIndex];
        footstepAudioSource.loop = true;
        footstepAudioSource.Play();
    }

    private void StopMoveFootstep()
    {
        if (!isPlayingMoveFootstep) return;

        footstepAudioSource.Stop();
        isPlayingMoveFootstep = false;
    }

    private void PlayChaseFootstep(int clipIndex)
    {
        if (isPlayingChaseFootstep || chaseFootstepClip.Length <= clipIndex) return;

        isPlayingChaseFootstep = true;
        footstepAudioSource.clip = chaseFootstepClip[clipIndex];
        footstepAudioSource.loop = true;
        footstepAudioSource.Play();
    }

    private void StopChaseFootstep()
    {
        if (!isPlayingChaseFootstep) return;

        footstepAudioSource.Stop();
        isPlayingChaseFootstep = false;
    }

    public void PlayJumpScareSound(int clipIndex)
    {
        if (jumpScareSoundClip.Length <= clipIndex) return;

        jumpScareAudioSource.clip = jumpScareSoundClip[clipIndex];
        jumpScareAudioSource.Play();
    }
}
