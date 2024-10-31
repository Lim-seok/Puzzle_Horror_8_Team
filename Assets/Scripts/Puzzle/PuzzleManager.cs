using System;
using System.Collections.Generic;

[Serializable]
public class PuzzleSwitchCell
{
    public string key;
    public bool state;
    public event Action<string, bool> switchEvent;

    public void ActivateEvent(bool state)
    {
        switchEvent?.Invoke(key, state);
    }
}

public class PuzzleManager : Singleton<PuzzleManager>
{
    public Dictionary<string, PuzzleSwitchCell> puzzleSwitch = new Dictionary<string, PuzzleSwitchCell>();

    public bool AddPuzzleSwitch(PuzzleSwitchCell cell)
    {
        if (puzzleSwitch.ContainsKey(cell.key)) return false;

        puzzleSwitch.Add(cell.key, cell);
        return true;
    }

    public bool CheckDictionary(string key)
    {
        if (puzzleSwitch.ContainsKey(key))
            return true;

        else
            return false;
    }

    public PuzzleSwitchCell GetSwitch(string key)
    {
        return puzzleSwitch[key];
    }

    public void SetPuzzleSwitchState(string key, bool state)
    {
        puzzleSwitch[key].state = state;
        puzzleSwitch[key].ActivateEvent(state);
    }
}