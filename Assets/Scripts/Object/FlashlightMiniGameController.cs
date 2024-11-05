using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashMiniGameController : MonoBehaviour
{
    public Image smallCircle;
    public Image bigCircle;
    public float shrinkSpeed = 1f;
    public int requiredSuccesses = 3;
    public int allowedFailures = 3;

    private int currentSuccessCount = 0;
    private int currentFailureCount = 0;

    private bool isMiniGameActive = false;
    private bool isCheckingTiming = false;

    public void StartMiniGame()
    {
        currentSuccessCount = 0;
        currentFailureCount = 0;
        isMiniGameActive = true;

        smallCircle.gameObject.SetActive(true);
        bigCircle.gameObject.SetActive(true);

        StartCoroutine(ShrinkBigCircle());
    }

    private IEnumerator ShrinkBigCircle()
    {
        while (isMiniGameActive)
        {
            bigCircle.transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

            // ������ ũ����� ��ҵǸ� Ÿ�̹� üũ �� ���
            if (bigCircle.transform.localScale.x <= 1f && !isCheckingTiming)
            {
                isCheckingTiming = true;
                CheckTiming();
                yield return new WaitForSeconds(1.5f); // �� ���� �� 0.5�� ���
                ResetBigCircle();
                isCheckingTiming = false;
            }

            yield return null;
        }
    }

    private void CheckTiming()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentSuccessCount++;
            Debug.Log("����!");

            if (currentSuccessCount >= requiredSuccesses)
            {
                EndMiniGame(true);
            }
        }
        else
        {
            currentFailureCount++;
            Debug.Log("����!");

            if (currentFailureCount >= allowedFailures)
            {
                EndMiniGame(false);
            }
        }
    }

    private void ResetBigCircle()
    {
        bigCircle.transform.localScale = Vector3.one * 3f;
    }

    private void EndMiniGame(bool isSuccess)
    {
        isMiniGameActive = false;

        smallCircle.gameObject.SetActive(false);
        bigCircle.gameObject.SetActive(false);

        if (isSuccess)
        {
            Debug.Log("�������� �ٽ� �������ϴ�!");
            // �������� �ٽ� �ѱ�
        }
        else
        {
            Debug.Log("���� ����!");
            // ���� ���� ó��
        }
    }
}
