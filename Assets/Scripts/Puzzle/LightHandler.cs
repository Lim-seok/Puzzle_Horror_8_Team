using System;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    [SerializeField] private ObstacleController controller;

    private void Awake()
    {
        gameObject.SetActive(false);
        controller.OnActivateEvent += TurnOnLight;
        controller.OnDeactivateEvent += TurnOffLight;
    }

    private void TurnOffLight()
    {
        gameObject.SetActive(false);
    }

    private void TurnOnLight()
    {
        gameObject.SetActive(true);
    }
}