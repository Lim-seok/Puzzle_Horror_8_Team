using UnityEngine;

public class LaserActivator : GenericActivator<LaserEmitter>
{
    protected override void SetActivation(bool isActivate)
    {
        base.SetActivation(isActivate);

        foreach (GameObject target in PuzzleObjects)
            target.SetActive(isActivate);

        foreach (LaserEmitter targetComp in puzzleComponents)
            targetComp.SetActivation(isActivate);
    }
}