using UnityEngine;

public class ItemHoldChecker : PuzzleBase
{
    private void Start()
    {
        CharacterManager.Instance.Player.interaction.OnHoldEvent += OnItemHold;
    }

    protected virtual void OnItemHold(bool isHolding)
    {
        if(CheckState() != isHolding)
            SetPuzzleState(isHolding);
    }
}