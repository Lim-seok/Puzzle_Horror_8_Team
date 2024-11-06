using System.Collections;
using UnityEngine;

public class SceneTimerEffectController : MonoBehaviour
{
    public VignetteBlinker blinker;

    private void Start()
    {
        StartCoroutine(StartEffectAfterDelay(5f));
    }

    private IEnumerator StartEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerEffect();
    }

    private void TriggerEffect()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Hit);
        blinker.BlinkVignette(5f);
    }
}
