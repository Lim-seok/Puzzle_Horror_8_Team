using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    public GameObject settingPanel;

    private void Start()
    {
        if (settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
        }
    }

    public void OnSettingPanel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        settingPanel.SetActive(true);
    }

    public void OnExitSettingPanel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sfx.Button);
        settingPanel.SetActive(false);
    }
}
