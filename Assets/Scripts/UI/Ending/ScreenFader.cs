using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    private float fadeTIme;
    private Image targetImage;

    private void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    public void SetTime(float time)
    {
        fadeTIme = time;
    }

    public void InitailizeFader(bool isFadeIn)
    {
        if (isFadeIn)
        {
            Color color = targetImage.color;
            color.a = 0.0f;
            targetImage.color = color;
        }

        else
        {
            Color color = targetImage.color;
            color.a = 1.0f;
            targetImage.color = color;
        }
    }

    public void FadeIn()
    {
        StartCoroutine (Fade(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine (Fade(1, 0));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentProgress = 0.0f;

        while(currentProgress < fadeTIme)
        {
            currentProgress += Time.deltaTime;

            Color color = targetImage.color;
            color.a = start - ((start - end) * (currentProgress / fadeTIme));
            targetImage.color = color;

            yield return null;
        }
    }
}