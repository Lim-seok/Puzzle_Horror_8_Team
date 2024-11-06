using UnityEngine;

public class JumpScareSound : MonoBehaviour
{
    public Animator animator;                
    public AudioSource audioSource;          
    public AudioClip[] randomAudioClips;            
    public string moveStateName = "Move";   

    private bool isPlayingMoveSound = false; 

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(moveStateName))
        {
            if (!isPlayingMoveSound)
            {
                PlayRandomMoveSound();  
            }
        }
        else
        {
            StopMoveSound();  
        }
    }

    private void PlayRandomMoveSound()
    {
        if (randomAudioClips.Length > 0)
        {
            int clipIndex = Random.Range(0, randomAudioClips.Length);  
            audioSource.clip = randomAudioClips[clipIndex];
            audioSource.Play();
            isPlayingMoveSound = true;
        }
    }

    private void StopMoveSound()
    {
        if (isPlayingMoveSound)
        {
            audioSource.Stop();
            isPlayingMoveSound = false;
        }
    }
}
