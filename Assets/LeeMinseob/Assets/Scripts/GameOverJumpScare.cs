using UnityEngine;
using System.Collections;

public class GameOverJumpScare : MonoBehaviour
{
    public GameObject creatureObject;
    public Transform playerTransform;
    public PlayerController playerController;

    public Vector3 cameraOffset = new Vector3(0.46f, 0.66f, 3.58f);
    public float cameraMoveSpeed = 1.0f;

    private Camera mainCamera;
    private bool isGameOverActive = false;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private Canvas[] uiCanvases;

    private void Awake()
    {
        mainCamera = Camera.main;
        creatureObject.SetActive(false);

        uiCanvases = FindObjectsOfType<Canvas>();
    }

    public void TriggerGameOver()
    {
        if (isGameOverActive) return;

        isGameOverActive = true;

        if (playerController != null)
        {
            playerController.enabled = false; 
        }

        creatureObject.transform.position = playerTransform.position + playerTransform.TransformDirection(cameraOffset);

        Vector3 creaturePosition = creatureObject.transform.position;
        creaturePosition.y -= 0.7f; 
        creatureObject.transform.position = creaturePosition;

        creatureObject.SetActive(true); 
        SetAllUICanvasesActive(false); 

        StartCoroutine(CameraToJumpScare());
    }

    private IEnumerator CameraToJumpScare()
    {

        Vector3 initialPosition = mainCamera.transform.position;
        Quaternion initialRotation = mainCamera.transform.rotation;

        Vector3 targetPosition = creatureObject.transform.position + creatureObject.transform.TransformDirection(cameraOffset);
        Quaternion targetRotation = Quaternion.LookRotation(creatureObject.transform.position - targetPosition);

        float elapsed = 0f;
        while (elapsed < cameraMoveSpeed)
        {
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / cameraMoveSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / cameraMoveSpeed);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
    }


    private void SetSkinnedMeshRendererActive(bool isActive)
    {
        foreach (var renderer in skinnedMeshRenderers)
        {
            renderer.enabled = isActive;
        }
    }

    private void SetAllUICanvasesActive(bool isActive)
    {
        foreach (var canvas in uiCanvases)
        {
            canvas.gameObject.SetActive(isActive);
        }
    }
}
