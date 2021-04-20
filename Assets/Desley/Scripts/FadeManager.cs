using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] float fadeTime;

    void Start()
    {
        StartCoroutine(FadeFromBlack(fadeTime));
    }

    public void StartFade() { StartCoroutine(Fade(fadeTime)); }

    IEnumerator Fade(float time)
    {
        StartCoroutine(FadeToBlack(time));

        yield return new WaitForSeconds(time);

        StartCoroutine(FadeFromBlack(time));
    }

    IEnumerator FadeFromBlack(float time)
    {
        Color alpha = blackImage.color;

        while (blackImage.color.a > 0)
        {
            alpha.a -= Time.deltaTime / time;
            blackImage.color = alpha;
            yield return null;
        }
    }

    IEnumerator FadeToBlack(float time)
    {
        Color alpha = blackImage.color;

        while (blackImage.color.a < 1)
        {
            alpha.a += Time.deltaTime / time;
            blackImage.color = alpha;
            yield return null;
        }
    }
}