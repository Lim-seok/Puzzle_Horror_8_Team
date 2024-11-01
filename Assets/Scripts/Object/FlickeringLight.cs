using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light targetLight;
    public Renderer targetRenderer;
    public float minFlickerInterval = 0.8f;
    public float maxFlickerInterval = 4f;
    public float flickerDuration = 0.2f;
    public Color emissionColor = Color.white;

    private Material targetMaterial;

    private void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
        }
        else
        {
            return;
        }

        targetMaterial.EnableKeyword("_EMISSION");

        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
            yield return new WaitForSeconds(flickerInterval);
            
            targetLight.enabled = false;
            targetMaterial.SetColor("_EmissionColor", Color.gray);

            yield return new WaitForSeconds(flickerDuration);

            targetLight.enabled = true;
            targetMaterial.SetColor("_EmissionColor", emissionColor);
        }
    }
}
