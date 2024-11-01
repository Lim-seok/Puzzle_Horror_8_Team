using Cinemachine;
using UnityEngine;

public class JumpScareEvent : MonoBehaviour
{
    public Animator monsterAnimator; 
    //public AudioSource jumpScareSound;
    private CinemachineImpulseSource impulseSource;

    private bool isJumpScareActive = false;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void TriggerJumpScare()
    {
        if (isJumpScareActive) return;

        isJumpScareActive = true;

        monsterAnimator.SetTrigger("JumpScare");

        // 점프스케어 사운드 재생
        //if (jumpScareSound != null)
        //{
        //}

        //impulseSource.GenerateImpulse();

    }

    private void GameOver()
    {

    }
}
