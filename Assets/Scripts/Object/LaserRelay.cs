using UnityEngine;
using System.Collections;

public class LaserRelay : MonoBehaviour
{
    public LineRenderer relayLaserRenderer;
    private bool isLit = false;
    private Vector3 lastIncomingDirection = Vector3.forward;
    private float laserTimeout = 0.2f;
    private float timeSinceLastRelay = 0f;

    private void Start()
    {
        relayLaserRenderer.enabled = false;
    }

    private void Update()
    {
        timeSinceLastRelay += Time.deltaTime;

        if (timeSinceLastRelay >= laserTimeout)
        {
            isLit = false;
            relayLaserRenderer.enabled = false;
        }
    }

    public void OnLaserHit(Vector3 incomingDirection)
    {
        if (isLit) return;

        isLit = true;
        lastIncomingDirection = incomingDirection;
        relayLaserRenderer.enabled = true;
        ResetLaserTimer();
        RelayLaser();
    }

    public void EnableRelayRenderer()
    {
        relayLaserRenderer.enabled = true;
    }

    private void RelayLaser()
    {
        if (!isLit) return;

        ResetLaserTimer();

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

    private void ResetLaserTimer()
    {
        timeSinceLastRelay = 0f;
    }
}
