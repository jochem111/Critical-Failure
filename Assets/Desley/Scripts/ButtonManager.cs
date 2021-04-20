using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] CanvasGroup settingsCanvasGroup;
    [SerializeField] Cinematics cinematics;

    [Space, SerializeField] float fadeTime;

    private void Start()
    {
        StartCoroutine(WaitForCinematic(cinematics.introTime));
    }

    public void StartGame()
    {
        StopAllCoroutines();

        StartCoroutine(FadeOut(fadeTime, mainCanvasGroup));
        StartCoroutine(FadeOut(fadeTime, settingsCanvasGroup));
        cinematics.ResumeCinematic();

        mainCanvasGroup.blocksRaycasts = false;
    }

    public void Settings()
    {
        StopAllCoroutines();

        StartCoroutine(FadeOut(fadeTime, mainCanvasGroup));
        StartCoroutine(FadeIn(fadeTime, settingsCanvasGroup));

        settingsCanvasGroup.blocksRaycasts = true;
    }

    public void BackButton()
    {
        StopAllCoroutines();

        StartCoroutine(FadeIn(fadeTime, mainCanvasGroup));
        StartCoroutine(FadeOut(fadeTime, settingsCanvasGroup));

        settingsCanvasGroup.blocksRaycasts = false;
    }

    public void QuitGame()
    {
        StopAllCoroutines();

        Application.Quit();
    }

    IEnumerator WaitForCinematic(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(FadeIn(fadeTime, mainCanvasGroup));

        mainCanvasGroup.blocksRaycasts = true;
    }

    IEnumerator FadeIn(float time, CanvasGroup canvasGroup)
    {
        float alpha = canvasGroup.alpha;

        while(canvasGroup.alpha < 1)
         {
            alpha += Time.deltaTime / time;
            canvasGroup.alpha = alpha;
            yield return null;
         }
    }

    IEnumerator FadeOut(float time, CanvasGroup canvasGroup)
    {
        float alpha = canvasGroup.alpha;

        while (canvasGroup.alpha > 0)
         {
            alpha -= Time.deltaTime / time;
            canvasGroup.alpha = alpha;
            yield return null;
         }
    }
}
