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
        settingPanel.SetActive(true);
    }

    public void OnExitSettingPanel()
    {
        settingPanel.SetActive(false);
    }
}
