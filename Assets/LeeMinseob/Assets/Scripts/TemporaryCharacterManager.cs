using UnityEngine;

public class TemporaryCharacterManager : Singleton<TemporaryCharacterManager>
{
    public TemporaryPlayer Player { get; private set; }

    private void Awake()
    {
        Player = FindObjectOfType<TemporaryPlayer>();

        // Player가 없는 경우 예외 처리
        if (Player == null)
        {
            Debug.LogError("Player instance not found in the scene.");
        }
    }
}
