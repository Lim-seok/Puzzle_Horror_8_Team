using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    private float currentTime = 60f;
    
    void Update()
    {
        if (currentTime > 0)
        {
            // 시간 감소
            currentTime -= Time.deltaTime;

            // 시간이 0 미만으로 내려가지 않도록 함
            if (currentTime < 0)
            {
                currentTime = 0;
            }

            // 타이머 텍스트 업데이트
            timerText.text = currentTime.ToString("F2");
        }
        if (currentTime <= 0)
        {
             EndGame();
        }

    }

    public void EndGame()
    {
        gameOverText.gameObject.SetActive(true);
        SceneManager.LoadScene("TitleScene");
    }
}
