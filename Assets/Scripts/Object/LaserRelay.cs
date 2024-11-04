using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class LaserRelay : MonoBehaviour, ILaserParts
{
    [SerializeField] private float rayCheckRate;
    private float rayCheckTime;
    private bool isLit = false;
    public float maxLaserDistance = 10f;

    public LineRenderer relayLaserRenderer;
    private ILaserParts rayHitComponent;
    
    private void Start()
    {
        relayLaserRenderer.enabled = false;
        rayCheckTime = Time.time;
    }

    private void Update()
    {
        if(isLit == true)
        {
            if(Time.time - rayCheckTime > rayCheckRate)
            {
                FireLaser();
            }
        }
    }

    public void OnLaserHit()
    {
        if (isLit) return;

        isLit = true;

        if(!relayLaserRenderer.enabled)
            relayLaserRenderer.enabled = true;
    }

    public void OnLaserMiss()
    {
        isLit = false;

        if (relayLaserRenderer.enabled)
            relayLaserRenderer.enabled = false;

        if (rayHitComponent != null)
        {
            rayHitComponent.OnLaserMiss();
            rayHitComponent = null;
        }
    }

    private void FireLaser()
    {
        if (!isLit) return;

        Vector3 relayDirection = transform.forward;
        RaycastHit hit;
        Vector3 start = transform.position;

        if (Physics.Raycast(start, relayDirection, out hit, maxLaserDistance))
        {
            relayLaserRenderer.SetPosition(0, start);
            relayLaserRenderer.SetPosition(1, hit.point);

            ILaserParts nextPart = hit.collider.GetComponent<ILaserParts>();
            
            if (nextPart != null)
            {
                if (rayHitComponent != nextPart)
                {
                    if (rayHitComponent != null)
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
            relayLaserRenderer.SetPosition(0, start);
            relayLaserRenderer.SetPosition(1, start + relayDirection * maxLaserDistance);
        }
    }
}
