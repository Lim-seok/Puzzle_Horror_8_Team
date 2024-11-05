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
        Debug.Log("손전등 고장 발생!");
        miniGameController.StartMiniGame();  // 미니게임 시작
    }
}
