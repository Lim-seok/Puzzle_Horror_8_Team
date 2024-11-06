using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    [SerializeField] private PuzzleSwitchCell cell;
    private bool isActivated;

    private void Awake()
    {
        PuzzleManager.Instance.AddPuzzleSwitch(cell);
        isActivated = true;
    }

    public void SetPuzzleState(bool state)
    {
        if (isActivated)
        {
            cell.state = state;
            cell.ActivateEvent(state);
        }
    }

    protected bool CheckState()
    {
        return cell.state;
    }

    public void SetActivation(bool activation)
    {
        if (activation == false)
            SetPuzzleState(false);

        isActivated = activation;
    }
}