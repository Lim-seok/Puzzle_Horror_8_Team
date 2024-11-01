using UnityEngine;

public class LightEmitter : MonoBehaviour
{
    public LineRenderer laserRenderer;
    public float maxLaserDistance = 10f;

    private void Start()
    {

    }

    private void Update()
    {
        FireLaser();
    }

    private void FireLaser()
    {
        RaycastHit hit;
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(start, direction, out hit, maxLaserDistance))
        {
            laserRenderer.SetPosition(0, start);
            laserRenderer.SetPosition(1, hit.point);

            LightRelay relay = hit.collider.GetComponent<LightRelay>();
            if (relay != null)
            {
                relay.OnLaserHit(direction);
            }
        }
        else
        {
            laserRenderer.SetPosition(0, start);
            laserRenderer.SetPosition(1, start + direction * maxLaserDistance);
        }
    }
}
