using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class FadeManager : MonoBehaviour
{
    [SerializeField] Image blackImage;
    public float fadeTime;
    private GameObject currentUi;

    void Start()
    {
        StartCoroutine(FadeFromBlack(fadeTime));
    }

    public void StartFade(GameObject cam, bool active, GameObject ui) 
    {
        StopAllCoroutines();

        currentUi = ui;
        Color alpha = blackImage.color;
        alpha.a = 0;
        blackImage.color = alpha;

        StartCoroutine(Fade(fadeTime, cam, active)); 
    }

    IEnumerator Fade(float time, GameObject cam, bool active)
    {
        StartCoroutine(FadeToBlack(time, cam, active));

        yield return new WaitForSeconds(time);

        StartCoroutine(FadeFromBlack(time));

        StopCoroutine(nameof(Fade));
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

        if (currentUi != null)
        {
            currentUi.SetActive(true);
            currentUi = null;
        }
        StopCoroutine(nameof(FadeFromBlack));
    }

    IEnumerator FadeToBlack(float time, GameObject cam, bool active)
    {
        Color alpha = blackImage.color;

        while (blackImage.color.a < 1)
        {
            alpha.a += Time.deltaTime / time;
            blackImage.color = alpha;
            yield return null;
        }

        if (cam)
        {
            if (cam.GetComponent<PlayableDirector>())
                cam.GetComponent<PlayableDirector>().Play();
            else
                cam.SetActive(active);

            cam = null;
        }

        StopCoroutine(nameof(FadeToBlack));
    }
}
