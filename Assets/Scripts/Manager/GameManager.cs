using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ButtonController[] buttons;
    private int activeButtonCount = 4;
    private bool isCheckingWinOrLose = false;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetIndex(i);
        }

        buttons[0].SetState(true);
        buttons[1].SetState(true);
        buttons[5].SetState(true);
        buttons[7].SetState(true);
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
            Debug.Log("Ŭ����!");
        }
        else if (activeButtonCount == 0)
        {
            Debug.Log("���� ����!");
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
