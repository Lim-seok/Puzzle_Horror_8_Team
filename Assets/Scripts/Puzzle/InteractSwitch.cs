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
        {
            cell.state = false;
            cell.ActivateEvent(false);
        }

        else
        {
            cell.state = true;
            cell.ActivateEvent(true);
        }
    }
}