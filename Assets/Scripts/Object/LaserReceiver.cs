using UnityEngine;

public class LaserReceiver : PuzzleBase, ILaserRecieve
{
    private GameObject clearPaticle;
    private Vector3 particlePosition;

    private void Start()
    {
        particlePosition = transform.position;
        particlePosition.y -= 1f;
    }

    public void OnLaserHit()
    {
        if (!CheckState())
        {
            SetPuzzleState(true);

            clearPaticle = ParticleManager.Instance.SpawnParticle("LaserReceiver", particlePosition, Quaternion.identity);
        }
    }

    public void OnLaserMiss()
    {
        if (CheckState())
        {
            SetPuzzleState(false);

            Destroy(clearPaticle);
        }
    }
}
