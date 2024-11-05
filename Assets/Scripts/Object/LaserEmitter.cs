using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    [SerializeField] private float rayCheckRate;
    private float rayCheckTime;

    public LineRenderer laserRenderer;
    protected ILaserRecieve rayHitComponent;
    public float maxLaserDistance = 10f;

    [SerializeField]private bool isActivated;

    private void Awake()
    {
        SetActivation(true);
    }

    protected virtual void Start()
    {
        rayCheckTime = Time.time;
    }

    protected virtual void Update()
    {
        if (isActivated)
            if (Time.time - rayCheckTime > rayCheckRate)
                FireLaser();
    }

    private void FireLaser()
    {
        Vector3 laserDirection = transform.forward;
        RaycastHit hit;
        Vector3 start = transform.position;

        if (Physics.Raycast(start, laserDirection, out hit, 10f))
        {
            laserRenderer.SetPosition(0, start);
            laserRenderer.SetPosition(1, hit.point);

            ILaserRecieve nextPart = hit.collider.GetComponent<ILaserRecieve>();

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
            if (rayHitComponent != null)
            {
                rayHitComponent.OnLaserMiss();
                rayHitComponent = null;
            }

            laserRenderer.SetPosition(0, start);
            laserRenderer.SetPosition(1, start + laserDirection * maxLaserDistance);
        }
    }

    public void SetActivation(bool activation)
    {
        if (activation == false)
        {
            if (rayHitComponent != null)
            {
                rayHitComponent.OnLaserMiss();
                rayHitComponent = null;
            }
        }

        laserRenderer.enabled = activation;
        isActivated = activation;
    }
}
