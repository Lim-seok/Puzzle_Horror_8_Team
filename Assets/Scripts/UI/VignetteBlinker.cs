using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class VignetteBlinker : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;

    private void Start()
    {
        volume.profile.TryGetSettings(out vignette);
    }

    public void BlinkVignette(float duration)
    {
        StartCoroutine(BlinkRoutine(duration));
    }

    private IEnumerator BlinkRoutine(float duration)
    {
        float elapsedTime = 0f;
        float pulseSpeed = 7f;

        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Beat);

        while (elapsedTime < duration)
        {
            vignette.intensity.value = Mathf.Lerp(0f, 0.5f, Mathf.Sin(elapsedTime * pulseSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = 0f;
    }
}
