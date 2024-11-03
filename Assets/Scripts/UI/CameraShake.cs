using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.3f;

    private Vector3 originalPosition;
    private bool isShaking = false; // 상태 변수 추가

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        if (!isShaking) // 현재 흔들림이 진행 중이 아닐 때만 실행
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

        // 원래 위치로 복구
        transform.localPosition = originalPosition;
        isShaking = false; // 상태 초기화
    }
}
