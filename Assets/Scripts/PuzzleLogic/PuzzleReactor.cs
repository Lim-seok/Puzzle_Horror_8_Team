using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleReactor : MonoBehaviour
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
    protected bool isActivated;

    private void Awake()
    {
        foreach (KeyContainer keyContainers in targetKeys)
        {
            isSwitchOn.Add(new bool[keyContainers.keys.Length]);
        }
    }

    protected virtual void Start()
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

        for (int j = 0; j < targetKeys.Length; j++)
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
    }

    protected bool CheckActivation(bool targetBool)
    {
        bool _isMatchedOnTarget = !targetBool;

        foreach (bool[] switches in isSwitchOn)
        {
            _isMatchedOnTarget = targetBool;

            foreach (bool isOn in switches)
            {
                if (isOn != targetBool)
                {
                    _isMatchedOnTarget = !targetBool;
                    break;
                }
            }

            if (_isMatchedOnTarget == targetBool)
                break;
        }

        return _isMatchedOnTarget;
    }
}