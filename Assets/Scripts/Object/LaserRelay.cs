using UnityEngine;

public class LaserRelay : MonoBehaviour
{
    public LineRenderer relayLaserRenderer;
    private bool isLit = false;
    private Vector3 lastIncomingDirection = Vector3.forward;
    private float currentRotation = 90f;

    private void Start()
    {
        relayLaserRenderer.enabled = false;
    }

    private void Update()
    {
        if (CheckLaserHit())
        {
            RelayLaser();
        }
        else
        {
            isLit = false;
            relayLaserRenderer.enabled = false;
        }
    }

    private bool CheckLaserHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - lastIncomingDirection * 0.1f, lastIncomingDirection, out hit, 1f))
        {
            LaserRelay previousRelay = hit.collider.GetComponent<LaserRelay>();
            if (previousRelay != null)
            {
                isLit = true;
                lastIncomingDirection = hit.normal;
                relayLaserRenderer.enabled = true;
                return true;
            }
        }
        return false;
    }

    public void RotateRelayDirection()
    {
        currentRotation += 90f;
        if (currentRotation >= 360f) currentRotation = 0f;

        if (CheckLaserHit())
        {
            RelayLaser();
        }
    }

    public void OnLaserHit(Vector3 incomingDirection)
    {
        isLit = true;
        lastIncomingDirection = incomingDirection;
        relayLaserRenderer.enabled = true;
        RelayLaser();
    }

    private void RelayLaser()
    {
        if (!isLit) return;

        Vector3 relayDirection = Quaternion.Euler(0, currentRotation, 0) * lastIncomingDirection;

        RaycastHit hit;
        Vector3 start = transform.position;

        if (Physics.Raycast(start, relayDirection, out hit, 10f))
        {
            relayLaserRenderer.SetPosition(0, start);
            relayLaserRenderer.SetPosition(1, hit.point);

            LaserReceiver receiver = hit.collider.GetComponent<LaserReceiver>();
            if (receiver != null)
            {
                receiver.OnLaserReceived();
            }
            else
            {
                LaserRelay nextRelay = hit.collider.GetComponent<LaserRelay>();
                if (nextRelay != null)
                {
                    nextRelay.OnLaserHit(relayDirection);
                }
            }
        }
        else
        {
            relayLaserRenderer.SetPosition(0, start);
            relayLaserRenderer.SetPosition(1, start + relayDirection * 10f);
        }
    }
}
