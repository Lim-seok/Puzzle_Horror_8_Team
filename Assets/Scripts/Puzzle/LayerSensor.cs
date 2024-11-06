using UnityEngine;

public class LayerSensor : PuzzleBase
{
    [SerializeField] private LayerMask targetLayers;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            SetPuzzleState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((targetLayers & (1 << other.gameObject.layer)) != 0) ;
        {
            SetPuzzleState(false);
        }
    }
}
