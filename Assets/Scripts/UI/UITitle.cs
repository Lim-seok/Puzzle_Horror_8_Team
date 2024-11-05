using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitle : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject savePanel;
    public GameObject newSaveSlot;

    public void OnSettingButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        settingPanel.SetActive(true);
    }

    public void OnExitSettingButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        settingPanel.SetActive(false);
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

    public void OnStartButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        savePanel.SetActive(true);
    }

    public void OnExitSaveSlotButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        savePanel.SetActive(false);
    }

    public void OnExitNewSaveSlotButtonPressed()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        newSaveSlot.SetActive(false);
    }

}
