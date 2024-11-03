using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.3f;

    private Vector3 originalPosition;
    private bool isShaking = false; // ���� ���� �߰�

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        if (!isShaking) // ���� ��鸲�� ���� ���� �ƴ� ���� ����
        {
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // ���� ��ġ�� ����
        transform.localPosition = originalPosition;
        isShaking = false; // ���� �ʱ�ȭ
    }
}
