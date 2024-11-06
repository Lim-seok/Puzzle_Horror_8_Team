using UnityEngine;

public class EndingEscaped : EndingTrapped
{
    private TextRaiser raiser;
    [SerializeField] protected float raiseStartTime;
    [SerializeField] protected float raiseEndTime;

    [SerializeField] protected GameObject fadeScreen2;
    protected ScreenFader? fader2;
    [SerializeField] protected float fadeStartTime2;
    [SerializeField] protected float fadeEndTime2;

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

        Invoke("CallFadeIn2", fadeStartTime2);

        Invoke("GotoTitle", endTime);
    }

    private void CallFadeIn2()
    {
        GameObject targetObj2 = Instantiate(fadeScreen2, targetParent);
        fader2 = targetObj2.GetComponent<ScreenFader>();

        fader2.SetTime(fadeEndTime2);
        fader2.FadeIn();
    }

    private void CallRaiseUp()
    {
        raiser.RaiseUp();
    }
}