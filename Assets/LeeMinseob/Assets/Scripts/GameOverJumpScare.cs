using UnityEngine;
using System.Collections;

public class GameOverEvent : MonoBehaviour
{
    public GameObject creaturePrefab; 
    public Transform creatureSpawnPoint; // �������� ��ȯ�� ��ġ
    //public PlayerController playerController; 
    public Vector3 cameraOffset = new Vector3(0.46f, 0.66f, 3.58f);
    public float cameraTransitionDuration = 1.0f;

    private GameObject spawnedGameOverObject;
    private Camera mainCamera;
    
    private bool isGameOverActive = false;

    private void Awake()
    {
        mainCamera = Camera.main; 
    }

    private void Start()
    {
        //�׽�Ʈ���� ���
        TriggerGameOver();
    }

    public void TriggerGameOver()
    {
        if (isGameOverActive) return;

        isGameOverActive = true;

        //if (playerController != null)
        //{
        //    playerController.enabled = false;
        //}

        spawnedGameOverObject = Instantiate(creaturePrefab, creatureSpawnPoint.position, creatureSpawnPoint.rotation);

 
        StartCoroutine(CameraToJumpScare());
    }

    private IEnumerator CameraToJumpScare()
    {
        Vector3 initialPosition = mainCamera.transform.position;
        Quaternion initialRotation = mainCamera.transform.rotation;

        Vector3 targetPosition = creatureSpawnPoint.position + creatureSpawnPoint.TransformDirection(cameraOffset);
        Quaternion targetRotation = Quaternion.LookRotation(creatureSpawnPoint.position - targetPosition);

        float elapsed = 0f;
        while (elapsed < cameraTransitionDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / cameraTransitionDuration);
            mainCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / cameraTransitionDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ī�޶� ������ ���ʿ�����?
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
    }
}
