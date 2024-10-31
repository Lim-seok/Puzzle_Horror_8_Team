using System;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private string[] targetKeys;
    private bool[] isSwitchOn;

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

        Debug.Log(key + isSolved);
    }
}