using System.Collections;
using UnityEngine;

public class LightRelay : MonoBehaviour
{
    public LineRenderer relayLaserRenderer;
    private bool isLit = false;
    private Vector3 lastIncomingDirection;
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
            LightRelay previousRelay = hit.collider.GetComponent<LightRelay>();
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
        RelayLaser(); // 각도가 변경될 때마다 RelayLaser를 업데이트
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
        Vector3 relayDirection = Quaternion.Euler(0, currentRotation, 0) * lastIncomingDirection;

        RaycastHit hit;
        Vector3 start = transform.position;

        if (Physics.Raycast(start, relayDirection, out hit, 10f))
        {
            relayLaserRenderer.SetPosition(0, start);
            relayLaserRenderer.SetPosition(1, hit.point);

            LightReceiver receiver = hit.collider.GetComponent<LightReceiver>();
            if (receiver != null)
            {
                // 완료인식
                receiver.OnLaserReceived();
            }
            else
            {
                LightRelay nextRelay = hit.collider.GetComponent<LightRelay>();
                if (nextRelay != null)
                {
                    // 중계기인식
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
