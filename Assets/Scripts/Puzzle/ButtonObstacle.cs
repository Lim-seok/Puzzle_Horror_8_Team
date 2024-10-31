using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObstacle : MonoBehaviour
{
    [SerializeField] private PuzzleSwitchCell cell;
    [SerializeField] private string switchKeyTag;

    // Start is called before the first frame update
    private void Awake()
    {
        PuzzleManager.Instance.AddPuzzleSwitch(cell);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(switchKeyTag))
            PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(switchKeyTag))
            PuzzleManager.Instance.SetPuzzleSwitchState(cell.key, false);
    }
}
