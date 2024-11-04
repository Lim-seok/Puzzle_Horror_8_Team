using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
    public void OnStartButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        // TODO :: ������ġ �����ϱ�
        SceneManager.LoadScene("MainScene");
    }
}
