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

    private void Awake()
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
            ParticleSystem particleSystem = particleObj.GetComponentInChildren<ParticleSystem>();

            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            else
            {
                Debug.LogWarning($"파티클 시스템이 확인되지 않음.");
            }
            // 객체 반환 추가 필요없을때 파괴하기용
            return particleObj;
        }
        else
        {
            Debug.LogWarning($"파티클 이름을 확인해주세요.");
            return null;
        }
    }
}
