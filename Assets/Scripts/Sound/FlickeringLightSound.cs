using System.Collections;
using UnityEngine;

public class FlickeringLightSound : MonoBehaviour
{
    [Header("Light Settings")]
    public Light targetLight;
    public Renderer targetRenderer;
    public float minFlickerInterval = 0.8f;
    public float maxFlickerInterval = 4f;
    public float flickerDuration = 0.2f;
    public Color emissionColor = Color.white;

    [Header("Sound Settings")]
    public AudioSource flickerAudioSource;
    public AudioClip[] flickerClips;

    private Material targetMaterial;

    private void Start()
    {
        // Light ¹× Renderer ¼³Á¤
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
            targetMaterial.EnableKeyword("_EMISSION");
        }

        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
            yield return new WaitForSeconds(flickerInterval);

            targetLight.enabled = false;
            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", Color.gray);
            }

            PlayFlickerSound();

            yield return new WaitForSeconds(flickerDuration);

            targetLight.enabled = true;
            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", emissionColor);
            }
        }
    }

    private void PlayFlickerSound()
    {
        if (flickerAudioSource != null && flickerClips.Length > 0)
        {
            int clipIndex = Random.Range(0, flickerClips.Length);
            flickerAudioSource.clip = flickerClips[clipIndex];
            flickerAudioSource.Play();
        }
    }
}
