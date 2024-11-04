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
        if (other.gameObject.CompareTag(switchKeyTag))
        {
            cell.state = true;
            cell.ActivateEvent(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(switchKeyTag))
        {
            cell.state = false;
            cell.ActivateEvent(false);
        }
    }
}
