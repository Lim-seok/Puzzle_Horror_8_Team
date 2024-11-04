using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    [SerializeField] private float rayCheckRate;
    private float rayCheckTime;

    public LineRenderer laserRenderer;
    private ILaserParts rayHitComponent;
    public float maxLaserDistance = 10f;

    private void Update()
    {
        if (Time.time - rayCheckTime > rayCheckRate)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        Vector3 relayDirection = transform.forward;
        RaycastHit hit;
        Vector3 start = transform.position;

        if (Physics.Raycast(start, relayDirection, out hit, 10f))
        {
            laserRenderer.SetPosition(0, start);
            laserRenderer.SetPosition(1, hit.point);

            ILaserParts nextPart = hit.collider.GetComponent<ILaserParts>();

            if (nextPart != null)
            {
                if (rayHitComponent != nextPart)
                {
                    if(rayHitComponent != null)
                        rayHitComponent.OnLaserMiss();
                    rayHitComponent = nextPart;
                    nextPart.OnLaserHit();
                }
            }

            else
            {
                if (rayHitComponent != null)
                {
                    rayHitComponent.OnLaserMiss();
                    rayHitComponent = null;
                }
            }
        }

        else
        {
            laserRenderer.SetPosition(0, start);
            laserRenderer.SetPosition(1, start + relayDirection * maxLaserDistance);
        }
    }
}
