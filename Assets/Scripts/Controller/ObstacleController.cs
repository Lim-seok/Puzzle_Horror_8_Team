using System;

public class ObstacleController : PuzzleReactor
{
    public event Action OnActivateEvent;
    public event Action OnDeactivateEvent;

    protected override void ApplySwitch(string key, bool isSolved)
    {
        base.ApplySwitch(key, isSolved);

        CallEvent();
    }

    private void CallEvent()
    {
        bool isAllTrue = CheckActivation(true);

        if (isAllTrue && !isActivated)
        {
            isActivated = true;
            OnActivateEvent?.Invoke();
        }

        else if (!isAllTrue && isActivated)
        {
            isActivated = false;
            OnDeactivateEvent?.Invoke();
        }
    }
}