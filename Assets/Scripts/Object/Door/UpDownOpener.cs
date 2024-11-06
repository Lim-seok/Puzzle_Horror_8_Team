using System.Collections;
using UnityEngine;

public class UpDownOpener : BaseOpener
{
    public override void SetProgress(float progress)
    {
        transform.localScale = new Vector3(1.0f, 1.0f - progress, 1.0f);
        openProgress = progress;
    }

    protected override IEnumerator OpenCoroutine()
    {
        while(openProgress < 1.0f)
        {
            openProgress += openSpeed * Time.deltaTime;

            transform.localScale = new Vector3(1.0f, 1.0f - openProgress, 1.0f);
            yield return null;
        }
    }

    protected override IEnumerator CloseCoroutine()
    {
        while (openProgress > 0.0f)
        {
            openProgress -= closeSpeed * Time.deltaTime;

            transform.localScale = new Vector3(1.0f, 1.0f - openProgress, 1.0f);
            yield return null;
        }
    }
}
