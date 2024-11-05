using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ButtonController[] buttons;
    private int activeButtonCount = -3;
    private bool isCheckingWinOrLose = false;

    public GameOverJumpScare gameOverJumpScare;
    public FrontBackOpener frontBackOpener;
    public FrontBackOpener frontBackOpenerright;
    public VignetteBlinker blinker;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetIndex(i);
        }

        //buttons[0].SetState(true);
        //buttons[1].SetState(true);
        //buttons[2].SetState(true);
        //buttons[3].SetState(true);
        //buttons[4].SetState(true);
  

        //buttons[5].SetState(true);
        //buttons[7].SetState(true);
    }

    public void UpdateButtonCount(bool isActivated)
    {
        activeButtonCount += isActivated ? 1 : -1;

        if (!isCheckingWinOrLose && (activeButtonCount == buttons.Length || activeButtonCount == 0))
        {
            StartCoroutine(CheckWinOrLose());
        }
    }

    private IEnumerator CheckWinOrLose()
    {
        isCheckingWinOrLose = true;
        yield return new WaitForSeconds(0.5f);

        if (activeButtonCount == buttons.Length)
        {
            StartCoroutine(frontBackOpener.OpenDoor());

            Debug.Log("클리어!");
        }
        else if (activeButtonCount == 0)
        {
            if (gameOverJumpScare != null)
            {
                gameOverJumpScare.TriggerGameOver();
                blinker.BlinkVignette(5f);

                // TODO :: 메인메뉴보내기
            }
            

        }

        isCheckingWinOrLose = false;
    }

    public void ToggleAdjacentButtons(int buttonIndex)
    {
        buttons[buttonIndex].ToggleState();

        int leftIndex = (buttonIndex == 0) ? buttons.Length - 1 : buttonIndex - 1;
        buttons[leftIndex].ToggleState();

        int rightIndex = (buttonIndex == buttons.Length - 1) ? 0 : buttonIndex + 1;
        buttons[rightIndex].ToggleState();
    }
}
