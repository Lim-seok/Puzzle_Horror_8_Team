using UnityEngine;

public class JumpScareSound : MonoBehaviour
{
    public Animator animator;               
    public AudioSource audioSource;         
    public AudioClip moveClip;              
    public string moveStateName = "Move";   

    private bool isPlayingMoveSound = false; 

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(moveStateName))
        {
            if (!isPlayingMoveSound)
            {
                PlayMoveSound();
            }
        }
        else
        {
            StopMoveSound();
        }
    }

    private void PlayMoveSound()
    {
        audioSource.clip = moveClip;
        audioSource.Play();
        isPlayingMoveSound = true;
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
