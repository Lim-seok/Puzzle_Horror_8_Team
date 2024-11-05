using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameOver : Singleton<GameOver>
{
    public TextMeshProUGUI gameOverText;
    private static bool isGameOver;

    protected override void Awake()
    {
        base.Awake();
        isGameOver = false;
    }
    public static bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver(bool state)
    {
        isGameOver = state;
        if (isGameOver)
        {
            ShowGameOver();
        }
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        SceneManager.LoadScene("TitleScene");
    }
}
