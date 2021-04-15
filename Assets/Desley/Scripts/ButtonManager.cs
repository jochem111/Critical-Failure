using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] CanvasGroup settingsCanvasGroup;

    [Space, SerializeField] float fadeTime;

    private void Start()
    {
        StartCoroutine(FadeIn(fadeTime, mainCanvasGroup));
    }

    public void StartGame()
    {
        StopAllCoroutines();

        StartCoroutine(FadeOut(fadeTime, mainCanvasGroup));

        mainCanvasGroup.blocksRaycasts = false;

        //start cinematic
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

        Debug.Log("QuitThatShit");

        Application.Quit();
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
