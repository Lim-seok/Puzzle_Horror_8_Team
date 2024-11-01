using System;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private string[] targetKeys;
    private bool[] isSwitchOn;
    private bool isActivated;

    public event Action OnActivateEvent;
    public event Action OnDeactivateEvent;

    private void Awake()
    {
        isSwitchOn = new bool[targetKeys.Length];
    }

    private void Start()
    {
        foreach (string targetKey in targetKeys)
        {
            if (PuzzleManager.Instance.CheckDictionary(targetKey))
            {
                PuzzleManager.Instance.GetSwitch(targetKey).switchEvent += ApplySwitch;
            }
        }
    }

    protected virtual void ApplySwitch(string key, bool isSolved)
    {
        for (int i = 0; i < targetKeys.Length; i++)
        {
            if (targetKeys[i] == key)
            {
                isSwitchOn[i] = isSolved;
                break;
            }
        }

        CallEvent();
    }

    protected virtual bool CheckActivation()
    {
        bool _isActivated = true;
        foreach (bool isOn in isSwitchOn)
        {
            if (!isOn)
            {
                _isActivated = false;
                break;
            }
        }
        return _isActivated;
    }

    private void CallEvent()
    {
        bool isAllTrue = CheckActivation();

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