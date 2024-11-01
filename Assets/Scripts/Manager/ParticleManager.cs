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

    // 인스펙터창에서 등록할 파티클 리스트
    public List<ParticlePrefab> particlePrefabs = new List<ParticlePrefab>();

    // 파티클 이름으로 프리팹을 찾기 위한 딕셔너리
    private Dictionary<string, GameObject> particleDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // 인스펙터에 적은 키와 파티클프리팹을 딕셔너리로 옮겨줌
        foreach (var prefab in particlePrefabs)
        {
            if (!particleDict.ContainsKey(prefab.name))
            {
                particleDict.Add(prefab.name, prefab.particlePrefab);
            }
        }
    }

    // 파티클 생성
    public void SpawnParticle(string particleName, Vector3 position, Quaternion rotation)
    {
        if (particleDict.ContainsKey(particleName))
        {
            GameObject particleObj = Instantiate(particleDict[particleName], position, rotation);
            ParticleSystem particleSystem = particleObj.GetComponent<ParticleSystem>();

            if (particleSystem != null)
            {
                StartCoroutine(StopAndDestroyParticle(particleSystem));
            }
            else
            {
                Debug.LogWarning($"파티클 시스템이 확인되지 않음.");
            }
        }
        else
        {
            Debug.LogWarning($"파티클 이름을 확인해주세요.");
        }
    }

    private IEnumerator StopAndDestroyParticle(ParticleSystem particleSystem)
    {
        yield return new WaitUntil(() => !particleSystem.isPlaying);

        Destroy(particleSystem.gameObject);
    }
}
