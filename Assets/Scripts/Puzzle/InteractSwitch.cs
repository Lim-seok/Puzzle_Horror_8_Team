using UnityEngine;

public class InteractSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleSwitchCell cell;

    private void Awake()
    {
        PuzzleManager.Instance.AddPuzzleSwitch(cell);
    }

    public string GetInteractPrompt()
    {
        return "E: 누르기";
    }

    public void OnInteract()
    {
        if (cell.state)
            PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, false);

        else
            PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, true);
    }
}