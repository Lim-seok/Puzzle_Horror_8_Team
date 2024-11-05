using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public void OnMainButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        SceneManager.LoadScene("TitleScene");
    }

    public void OnQuitButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
