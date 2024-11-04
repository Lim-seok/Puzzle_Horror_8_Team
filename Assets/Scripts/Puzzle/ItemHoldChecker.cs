using UnityEngine;

public class ItemHoldChecker : MonoBehaviour
{
    [SerializeField] private PuzzleSwitchCell cell;

    private void Awake()
    {
        PuzzleManager.Instance.AddPuzzleSwitch(cell);
    }

    private void Start()
    {
        CharacterManager.Instance.Player.interaction.OnHoldEvent += OnItemHold;
    }

    protected virtual void OnItemHold(bool isHolding)
    {
        PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, isHolding);
    }
}