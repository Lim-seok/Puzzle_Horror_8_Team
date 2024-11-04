using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExit : MonoBehaviour
{
    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
