using System.Collections;
using UnityEngine;

public class BaseOpener : MonoBehaviour
{
    protected bool isOpened = false;
    protected float openProgress;
    [SerializeField] protected float openSpeed;
    [SerializeField] protected float closeSpeed;
    private Coroutine coroutine;

    [SerializeField] private bool isInitialOpened;
    private ObstacleController controller;

    private void Awake()
    {
        controller = GetComponent<ObstacleController>();
    }

    protected virtual void Start()
    {
        if (!isInitialOpened)
        {
            controller.OnActivateEvent += Open;
            controller.OnDeactivateEvent += Close;
            openProgress = 1.0f;
        }

        else
        {
            controller.OnActivateEvent += Close;
            controller.OnDeactivateEvent += Open;
            openProgress = 0.0f;
        }

        SetProgress(openProgress);
    }

    public virtual void SetProgress(float progress)
    {
        openProgress = progress;
    }

    public void Open()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(OpenCoroutine());
    }

    protected virtual IEnumerator OpenCoroutine()
    {
        yield return null;
    }

    public void Close()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CloseCoroutine());
    }

    protected virtual IEnumerator CloseCoroutine()
    {
        yield return null;
    }
}