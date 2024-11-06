using UnityEngine;

public class PuzzleActivator : GenericActivator<PuzzleBase>
{
    protected override void SetActivation(bool isActivate)
    {
        base.SetActivation(isActivate);

        foreach (GameObject target in PuzzleObjects)
            target.SetActive(isActivate);

        foreach (PuzzleBase targetComp in puzzleComponents)
            targetComp.SetActivation(isActivate);
    }
}