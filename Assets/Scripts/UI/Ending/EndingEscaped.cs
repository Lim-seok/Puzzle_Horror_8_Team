using UnityEngine;

public class EndingEscaped : EndingTrapped
{
    private TextRaiser raiser;
    [SerializeField] protected float raiseStartTime;
    [SerializeField] protected float raiseEndTime;

    public override void OnEndingTrigger()
    {
        GameObject targetObj = Instantiate(fadeScreen, targetParent);
        fader = targetObj.GetComponent<ScreenFader>();
        raiser = targetObj.GetComponentInChildren<TextRaiser>();

        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Ending);

        fader.SetTime(fadeEndTime);
        fader.InitailizeFader(true);
        Invoke("CallFadeIn", fadeStartTime);

        raiser.SetTime(raiseEndTime);
        raiser.InitailizeRaiser(true);
        Invoke("CallRaiseUp", raiseStartTime);

        Invoke("GotoTitle", endTime);
    }

    private void CallRaiseUp()
    {
        raiser.RaiseUp();
    }
}