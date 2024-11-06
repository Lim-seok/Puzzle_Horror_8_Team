using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrapped : MonoBehaviour
{
    [SerializeField] protected Transform targetParent;
    [SerializeField] protected GameObject fadeScreen;
    protected ScreenFader? fader;
    private ObstacleController controller;
    [SerializeField] protected float endTime;
    [SerializeField] protected float fadeStartTime;
    [SerializeField] protected float fadeEndTime;

    private void Awake()
    {
        controller = GetComponent<ObstacleController>();
    }

    private void Start()
    {
        controller.OnActivateEvent += OnEndingTrigger;
    }

    public virtual void OnEndingTrigger()
    {
        fader = Instantiate(fadeScreen, targetParent).GetComponent<ScreenFader>();

        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Ending);

        fader.SetTime(fadeEndTime);
        fader.InitailizeFader(true);
        Invoke("CallFadeIn", fadeStartTime);

        Invoke("GotoTitle", endTime);
    }

    protected void CallFadeIn()
    {
        fader.FadeIn();
    }

    protected void GotoTitle()
    {
        Cursor.lockState = CursorLockMode.None;
        PuzzleManager.Instance.InitializePuzzle();
        SceneManager.LoadScene("TitleScene");
    }
}
