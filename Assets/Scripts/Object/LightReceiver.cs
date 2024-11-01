using UnityEngine;

public class LightReceiver : MonoBehaviour
{
    public void OnLaserReceived()
    {
        CompleteQuest();
    }

    private void CompleteQuest()
    {
        Debug.Log("퀘스트 완료!");
    }
}
