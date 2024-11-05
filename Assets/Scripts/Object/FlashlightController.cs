using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private FlashMiniGameController miniGameController;

    private void Start()
    {
        miniGameController = GetComponent<FlashMiniGameController>();

        Invoke("TriggerMalfunction", 5f);
    }

    public void TriggerMalfunction()
    {
        Debug.Log("������ ���� �߻�!");
        miniGameController.StartMiniGame();  // �̴ϰ��� ����
    }
}
