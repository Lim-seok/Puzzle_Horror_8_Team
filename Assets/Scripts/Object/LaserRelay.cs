using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public interface ILaserRecieve
{
    public void OnLaserHit();
    public void OnLaserMiss();
}

public class LaserRelay : LaserEmitter, ILaserRecieve
{
    private bool isLit = false;
    
    protected override void Start()
    {
        laserRenderer.enabled = false;
    }

    protected override void Update()
    {
        if(isLit == true)
        {
            base.Update();
        }
    }

    public void OnLaserHit()
    {
        if (isLit) return;

        isLit = true;

        if(!laserRenderer.enabled)
            laserRenderer.enabled = true;
    }

    public void OnLaserMiss()
    {
        isLit = false;

        if (laserRenderer.enabled)
            laserRenderer.enabled = false;

        if (rayHitComponent != null)
        {
            rayHitComponent.OnLaserMiss();
            rayHitComponent = null;
        }
    }
}
