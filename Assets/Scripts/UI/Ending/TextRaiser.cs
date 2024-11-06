using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextRaiser : MonoBehaviour
{
    private float raiseTime;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;
    private TextMeshProUGUI targetText;
    private RectTransform rectTransform;

    private void Awake()
    {
        targetText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetTime(float time)
    {
        raiseTime = time;
    }

    public void InitailizeRaiser(bool isRaiseUp)
    {
        if (isRaiseUp)
        {
            Color color = targetText.color;
            color.a = 0.0f;
            targetText.color = color;

            rectTransform.anchoredPosition = startPosition;
        }

        else
        {
            Color color = targetText.color;
            color.a = 1.0f;
            targetText.color = color;

            rectTransform.anchoredPosition = endPosition;
        }
    }

    public void RaiseUp()
    {
        StartCoroutine(Fade(0, 1));
        StartCoroutine(Raise(startPosition, endPosition));
    }

    public void FallDown()
    {
        StartCoroutine(Fade(1, 0));
        StartCoroutine(Raise(endPosition, startPosition));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentProgress = 0.0f;

        while (currentProgress < raiseTime)
        {
            currentProgress += Time.deltaTime;

            Color color = targetText.color;
            color.a = start - ((start - end) * (currentProgress / raiseTime));
            targetText.color = color;

            yield return null;
        }
    }

    private IEnumerator Raise(Vector2 start, Vector2 end)
    {
        float currentProgress = 0.0f;

        while (currentProgress < raiseTime)
        {
            currentProgress += Time.deltaTime;

            rectTransform.anchoredPosition = start - ((start - end) * (currentProgress / raiseTime));

            yield return null;
        }
    }
}