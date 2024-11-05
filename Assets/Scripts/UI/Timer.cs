using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float gameTime = 60f;
    private float currentTime;
    private bool isGameOver = false;

    void Start()
    {
        currentTime = gameTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
         currentTime -= Time.deltaTime;
         UpdateTimerDisplay();

         if (currentTime <= 0)
         {
             EndGame();
         }
    }

    void UpdateTimerDisplay()
    {
        if (currentTime < 0)
            currentTime = 0;

        timerText.text = currentTime.ToString("F2");
    }
    void EndGame()
    {
        GameOver.Instance.SetGameOver(true);
    }
}
