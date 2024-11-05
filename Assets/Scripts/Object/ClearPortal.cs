using UnityEngine;

public class ClearPortal : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            LevelController.Instance.NextLevel();
        }
    }
}