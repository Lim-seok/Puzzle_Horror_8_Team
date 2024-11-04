using UnityEngine;

public class InteractSwitch : PuzzleBase, IInteractable
{
    public string GetInteractPrompt()
    {
        return "E: 누르기";
    }

    public void OnInteract()
    {
        SetPuzzleState(!CheckState());
    }
}