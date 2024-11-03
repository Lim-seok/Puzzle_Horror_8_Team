using System;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] lightObjs;
    [SerializeField] private ObstacleController controller;
    [SerializeField] private bool isInitialOn;

    private void Awake()
    {
        foreach (GameObject light in lightObjs)
            light.SetActive(isInitialOn);

        controller.OnActivateEvent += SetSwitch;
        controller.OnDeactivateEvent += UnsetSwitch;
    }

    private void UnsetSwitch()
    {
        foreach(GameObject light in lightObjs)
            light.SetActive(isInitialOn);
    }

    private void SetSwitch()
    {
        foreach (GameObject light in lightObjs)
            light.SetActive(!isInitialOn);
    }
}