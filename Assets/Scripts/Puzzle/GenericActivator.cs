using UnityEngine;

public class GenericActivator<T> : PuzzleReactor
{
    [SerializeField] protected GameObject[] PuzzleObjects;
    [SerializeField] protected T[] puzzleComponents;
    [SerializeField] protected bool isInitialOn;

    protected override void Start()
    {
        base.Start();
        SetActivation(isInitialOn);
    }

    protected override void ApplySwitch(string key, bool isSolved)
    {
        base.ApplySwitch(key, isSolved);

        bool isAllTrue = CheckActivation(true);

        if (isAllTrue && !isActivated)
            SetActivation(!isInitialOn);

        else if (!isAllTrue && isActivated)
            SetActivation(isInitialOn);
    }

    protected virtual void SetActivation(bool isActivate)
    {
        isActivated = isActivate;
    }
}