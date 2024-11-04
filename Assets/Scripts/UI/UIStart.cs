using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
    public void OnStartButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        // TODO :: 저장위치 선택하기
        SceneManager.LoadScene("MainScene");
    }
}
