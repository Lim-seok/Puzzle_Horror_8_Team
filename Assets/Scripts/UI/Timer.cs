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
            // �ð� ����
            currentTime -= Time.deltaTime;

            // �ð��� 0 �̸����� �������� �ʵ��� ��
            if (currentTime < 0)
            {
                currentTime = 0;
            }

            // Ÿ�̸� �ؽ�Ʈ ������Ʈ
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
