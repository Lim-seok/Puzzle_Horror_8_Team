using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;
    private float nextStepTime = 0f;

    private void Update()
    {
        if (IsWalking() && Time.time >= nextStepTime)
        {
            PlayFootstep();
            nextStepTime = Time.time + stepInterval;
        }
    }

    private bool IsWalking()
    {
        return Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
    }

    private void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int clipIndex = Random.Range(0, footstepClips.Length);
            footstepAudioSource.clip = footstepClips[clipIndex];
            footstepAudioSource.Play();
        }
    }
}
