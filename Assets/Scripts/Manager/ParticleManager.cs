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

    // �ν�����â���� ����� ��ƼŬ ����Ʈ
    public List<ParticlePrefab> particlePrefabs = new List<ParticlePrefab>();

    // ��ƼŬ �̸����� �������� ã�� ���� ��ųʸ�
    private Dictionary<string, GameObject> particleDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // �ν����Ϳ� ���� Ű�� ��ƼŬ�������� ��ųʸ��� �Ű���
        foreach (var prefab in particlePrefabs)
        {
            if (!particleDict.ContainsKey(prefab.name))
            {
                particleDict.Add(prefab.name, prefab.particlePrefab);
            }
        }
    }

    // ��ƼŬ ����
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
                Debug.LogWarning($"��ƼŬ �ý����� Ȯ�ε��� ����.");
            }
        }
        else
        {
            Debug.LogWarning($"��ƼŬ �̸��� Ȯ�����ּ���.");
        }
    }

    private IEnumerator StopAndDestroyParticle(ParticleSystem particleSystem)
    {
        yield return new WaitUntil(() => !particleSystem.isPlaying);

        Destroy(particleSystem.gameObject);
    }
}
