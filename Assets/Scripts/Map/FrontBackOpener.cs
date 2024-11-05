using System.Collections;
using UnityEngine;

public class FrontBackOpener : BaseOpener
{
    [SerializeField] private bool isRight;
    [SerializeField] private float maxAngle;
    private float defaultAngle = -90;
    private int sign;

    protected override void Start()
    {
        if (isRight)
            sign = -1;
        else
            sign = 1;

        base.Start();
    }

    public IEnumerator OpenDoor()
    {
        yield return StartCoroutine(OpenCoroutine());
    }

    public override void SetProgress(float progress)
    {
        transform.localRotation = Quaternion.Euler(0.0f, defaultAngle + (sign * maxAngle * openProgress), 0.0f);
        openProgress = progress;
    }

    protected override IEnumerator OpenCoroutine()
    {
        while (openProgress < 1.0f)
        {
            openProgress += openSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0.0f, defaultAngle + (sign * maxAngle * openProgress), 0.0f);
            yield return null;
        }
    }

    protected override IEnumerator CloseCoroutine()
    {
        while (openProgress > 0.0f)
        {
            openProgress -= closeSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0.0f, defaultAngle + (sign * maxAngle * openProgress), 0.0f);
            yield return null;
        }
    }

    
}