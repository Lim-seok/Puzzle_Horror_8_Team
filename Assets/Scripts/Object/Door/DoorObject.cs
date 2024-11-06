using System.Collections;
using UnityEngine;

public class DoorObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected bool isOpened = false;
    protected float openProgress;
    [SerializeField] protected float openSpeed;
    [SerializeField] protected float closeSpeed;
    private Coroutine coroutine;

    [SerializeField] private bool isInitialOpened;

    [SerializeField] private bool isRight;
    [SerializeField] private float maxAngle;
    private float defaultAngle = 0;
    private int sign;

    private void Start()
    {
        if (isInitialOpened)
        {
            isOpened = true;
            openProgress = 1.0f;
        }

        else
        {
            isOpened = false;
            openProgress = 0.0f;
        }

        if (isRight)
            sign = -1;

        else
            sign = 1;

        SetProgress(openProgress);
    }

    public string GetInteractPrompt()
    {
        if (isOpened)
            return "E: 닫기";

        else
            return "E: 열기";
    }

    public void OnInteract()
    {
        if (isOpened)
        {
            Close();
            AudioManager.Instance.PlaySFX(AudioManager.Sfx.CloseDoor);
        }

        else
        {
            Open();
            AudioManager.Instance.PlaySFX(AudioManager.Sfx.OpenDoor);
        }
    }

    public virtual void SetProgress(float progress)
    {
        transform.localRotation = Quaternion.Euler(0.0f, defaultAngle + (sign * maxAngle * openProgress), 0.0f);
        openProgress = progress;
    }

    public void Open()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(OpenCoroutine());
        isOpened = true;
    }

    protected virtual IEnumerator OpenCoroutine()
    {
        while (openProgress <= 1.0f)
        {
            openProgress += openSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0.0f, defaultAngle + (sign * maxAngle * openProgress), 0.0f);
            yield return null;
        }
    }

    public void Close()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CloseCoroutine());
        isOpened = false;
    }

    protected virtual IEnumerator CloseCoroutine()
    {
        while (openProgress >= 0.0f)
        {
            openProgress -= closeSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0.0f, defaultAngle + (sign * maxAngle * openProgress), 0.0f);
            yield return null;
        }
    }
}
