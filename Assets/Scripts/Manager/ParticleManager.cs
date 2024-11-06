using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    [System.Serializable]
    public class ParticlePrefab
    {
        public string name;
        public GameObject particlePrefab;
    }

    public List<ParticlePrefab> particlePrefabs = new List<ParticlePrefab>();

    private Dictionary<string, GameObject> particleDict = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (var prefab in particlePrefabs)
        {
            if (!particleDict.ContainsKey(prefab.name))
            {
                particleDict.Add(prefab.name, prefab.particlePrefab);
            }
        }
    }

    public GameObject SpawnParticle(string particleName, Vector3 position, Quaternion rotation)
    {
        if (particleDict.ContainsKey(particleName))
        {
            GameObject particleObj = Instantiate(particleDict[particleName], position, rotation);
            ParticleSystem particleSystem = particleObj.GetComponent<ParticleSystem>();

            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            return particleObj;
        }
        else
        {
            return null;
        }
    }
}
