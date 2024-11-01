using UnityEngine;

public class TemporaryCharacterManager : Singleton<TemporaryCharacterManager>
{
    public TemporaryPlayer Player { get; private set; }

    private void Awake()
    {
        Player = FindObjectOfType<TemporaryPlayer>();

        // Player�� ���� ��� ���� ó��
        if (Player == null)
        {
            Debug.LogError("Player instance not found in the scene.");
        }
    }
}
