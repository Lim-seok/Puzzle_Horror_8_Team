using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Serializable]
    public struct KeyContainer
    {
        public string[] keys;

        public KeyContainer(string[] _targetKeys)
        {
            keys = _targetKeys.ToArray();
        }
    }

    [SerializeField] private KeyContainer[] targetKeys;
    private List<bool[]> isSwitchOn = new List<bool[]>();
    private bool isActivated;

    public event Action OnActivateEvent;
    public event Action OnDeactivateEvent;

    private void Awake()
    {
        foreach (KeyContainer keyContainers in targetKeys)
        {
            isSwitchOn.Add(new bool[keyContainers.keys.Length]);
        }
    }

    private void Start()
    {
        foreach (KeyContainer keys in targetKeys)
        {
            foreach (string targetKey in keys.keys)
            {
                if (PuzzleManager.Instance.CheckDictionary(targetKey))
                {
                    PuzzleManager.Instance.GetSwitch(targetKey).switchEvent += ApplySwitch;
                }
            }
        }
    }

    protected virtual void ApplySwitch(string key, bool isSolved)
    {
        bool isFound = false;

        for(int j = 0; j < targetKeys.Length; j++)
        {    
            for (int i = 0; i < targetKeys[j].keys.Length; i++)
            {
                if (targetKeys[j].keys[i] == key)
                {
                    isSwitchOn[j][i] = isSolved;
                    isFound = true;
                    break;
                }
            }

            if (isFound)
                break;
        }

        CallEvent();
    }

    protected virtual bool CheckActivation()
    {
        bool _isActivated = false;

        foreach (bool[] switches in isSwitchOn)
        {
            _isActivated = true;

            foreach (bool isOn in switches)
            {
                if (!isOn)
                {
                    _isActivated = false;
                    break;
                }
            }

            if (_isActivated)
                break;
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