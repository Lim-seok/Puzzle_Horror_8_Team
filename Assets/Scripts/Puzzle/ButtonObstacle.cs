using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObstacle : PuzzleBase
{
    [SerializeField] private string switchKeyTag;
    [SerializeField] private int cubeAmount;

    private void Start()
    {
        cubeAmount = 0;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(switchKeyTag))
        {
            if(cubeAmount == 0)
                SetPuzzleState(true);

            cubeAmount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(switchKeyTag))
        {
            cubeAmount--;

            if (cubeAmount < 0)
                cubeAmount = 0;

            if (cubeAmount == 0)
                SetPuzzleState(false);
        }
    }
}
