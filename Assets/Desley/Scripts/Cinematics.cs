using Cinemachine;
using System.Collections;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] GameObject mainMenu, settingsMenu;

    [Space, SerializeField] CanvasGroup skipCanvasGroup;
    [SerializeField] float skipTime = 1;

    float originalCartSpeed;

    public float introTime;
    public float resumeTime;

    bool showSkipText = true;
    bool skipped;

    void Start()
    {
        playerMove.AllowMovement(false);

        originalCartSpeed = dollyCart.m_Speed;

        StartCoroutine(DisableSkip(introTime - 1));
        StartCoroutine(WaitForPauseTime(introTime));

        StartSkipFading();
    }

    IEnumerator DisableSkip(float time)
    {
        yield return new WaitForSeconds(time);

        Manager.manager.interact.canSkip = false;
    }

    IEnumerator WaitForPauseTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (!skipped)
        {
            Manager.manager.interact.canSkip = false;
            Manager.manager.interact.index++;

            PauseCinematic();
        }
    }

    public void PauseCinematic() 
    { 
        dollyCart.m_Speed = 0;

        showSkipText = false;
        skipped = true;

        StopCoroutine(nameof(WaitForPauseTime));
    }

    public void ResumeCinematic() 
    { 
        dollyCart.m_Speed = originalCartSpeed;

        showSkipText = true;
        StartSkipFading();

        Manager.manager.interact.canSkip = true;

        StartCoroutine(DisableSkip(resumeTime - 1));

        StartCoroutine(EndCinematic(resumeTime));
    }

    IEnumerator EndCinematic(float time)
    {
        yield return new WaitForSeconds(time);

        DisableCinematic();
    }

    public void DisableCinematic()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        skipCanvasGroup.gameObject.SetActive(false);

        playerMove.AllowMovement(true);
        gameObject.SetActive(false);
    }

    public void StartSkipFading() { StartCoroutine(SkipFading(skipTime)); }

    IEnumerator SkipFading(float time)
    {
        float alpha = skipCanvasGroup.alpha;

        while (skipCanvasGroup.alpha < 1)
        {
            alpha += Time.deltaTime / time;
            skipCanvasGroup.alpha = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(time);

        alpha = skipCanvasGroup.alpha;

        while (skipCanvasGroup.alpha > 0)
        {
            alpha -= Time.deltaTime / time;
            skipCanvasGroup.alpha = alpha;
            yield return null;
        }

        if(showSkipText)
            StartCoroutine(SkipFading(skipTime));
    }
}
