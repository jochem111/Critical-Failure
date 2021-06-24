using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] CanvasGroup mainCanvasGroup;
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
        cinematics.ResumeCinematic();

        mainCanvasGroup.blocksRaycasts = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        StopAllCoroutines();

        Application.Quit();
    }

    IEnumerator WaitForCinematic(float time)
    {
        yield return new WaitForSeconds(time);

        FadeMainMenu();
    }

    public void FadeMainMenu()
    {
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
