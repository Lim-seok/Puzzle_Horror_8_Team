using UnityEngine;

public class SceneTimerEffectController : MonoBehaviour
{
    public VignetteBlinker blinker;

    private float sceneTimer = 0f;
    public float effectTriggerTime = 5f;

    private void Start()
    {
        sceneTimer = 0f;
    }

    private void Update()
    {
        sceneTimer += Time.deltaTime;

        if (sceneTimer >= effectTriggerTime)
        {
            TriggerEffect();
        }
    }

    private void TriggerEffect()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Hit);
        blinker.BlinkVignette(3f);
    }
}
