using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] CanvasGroup settingsCanvasGroup;

    [Space, SerializeField] float introTime;
    [SerializeField] float transitionTime;
    [SerializeField] float fadeTime;

    private void Start()
    {
        StartCoroutine(WaitForTimeline(introTime));
    }

    public void StartGame()
    {
        StopAllCoroutines();

        StartCoroutine(FadeOut(fadeTime, mainCanvasGroup));
        StartCoroutine(DisableCinemachineBrain(transitionTime));

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

    IEnumerator WaitForTimeline(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(FadeIn(fadeTime, mainCanvasGroup));
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

    IEnumerator DisableCinemachineBrain(float time)
    {
        yield return new WaitForSeconds(time);

        cam.GetComponent<CinemachineBrain>().enabled = false;
    }
}
