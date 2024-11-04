using Cinemachine;
using System.Collections;
using UnityEngine;

public class JumpScareEvent : MonoBehaviour
{
    public Animator creatureAnimator;
    public Transform creatureTransform;
    public Transform playerTransform;
    public PlayerController playerController;
    public float cameraTransitionDuration = 1.0f;


    public Transform headTarget;

    private Camera mainCamera;
    private CinemachineVirtualCamera creatureVirtualCamera;
    private CinemachineVirtualCamera playervirtualCamera;
    private CinemachineImpulseSource impulseSource;

    private bool isJumpScareActive = false;

    private int originalPriority;

    private CameraShake cameraShake;

    private void Awake()
    {
        creatureVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        cameraShake = creatureVirtualCamera.GetComponent<CameraShake>();

        if (creatureVirtualCamera != null)
        {
            originalPriority = creatureVirtualCamera.Priority; 
            creatureVirtualCamera.Priority = 0; 
        }
    }

    public void TriggerJumpScare()
    {
        if (isJumpScareActive) return;

        isJumpScareActive = true;
        playerController.enabled = false;
        creatureAnimator.SetTrigger("JumpScare");

        if (creatureVirtualCamera != null)
        {
            creatureVirtualCamera.Priority = 100; 
            creatureVirtualCamera.LookAt = headTarget; 
        }

        if (cameraShake != null)
        {
            cameraShake.TriggerShake();
        }

        StartCoroutine(HandleJumpScare());
    }

    private IEnumerator HandleJumpScare()
    {
        float timeElapsed = 0f;
        while (timeElapsed < cameraTransitionDuration)
        {
            //추가적인 시각 효과는 여기에

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (creatureVirtualCamera != null)
        {
            creatureVirtualCamera.Priority = originalPriority;
        }

        playerController.enabled = true; 
        isJumpScareActive = false;

    }

   

}
