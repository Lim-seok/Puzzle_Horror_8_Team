using UnityEngine;

public class LaserRelay : MonoBehaviour
{
    public LineRenderer relayLaserRenderer;
    private bool isLit = false;
    private Vector3 lastIncomingDirection = Vector3.forward;
    private bool laserReceivedThisFrame = false;

    private void Start()
    {
        relayLaserRenderer.enabled = false;
    }

    private void Update()
    {
        if (!laserReceivedThisFrame)
        {
            isLit = false;
            relayLaserRenderer.enabled = false;
        }
        laserReceivedThisFrame = false;
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

    public void OnLaserHit(Vector3 incomingDirection)
    {
        if (isLit) return;

        isLit = true;
        lastIncomingDirection = incomingDirection;
        relayLaserRenderer.enabled = true;
        laserReceivedThisFrame = true;
        RelayLaser();
    }

    public void EnableRelayRenderer()
    {
        relayLaserRenderer.enabled = true;
    }

    private void RelayLaser()
    {
        if (!isLit) return;

        Vector3 relayDirection = transform.forward;
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
                if (nextRelay != null && !nextRelay.isLit)
                {
                    nextRelay.EnableRelayRenderer();
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
