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

    [Space, SerializeField] float dollyCartPos;
    public bool canSkip = true;
    float index;

    void Start()
    {
        playerMove.AllowMovement(false);

        originalCartSpeed = dollyCart.m_Speed;

        StartCoroutine(DisableSkip(introTime - 1.5f));
        StartCoroutine(WaitForPauseTime(introTime));

        StartSkipFading();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && canSkip && index < 2)
            SkipCinematic();
    }

    //Function to skip the cinematic
    void SkipCinematic()
    {
        index++;
        canSkip = false;

        Manager.manager.fadeManager.StartFade(null, true, null);

        StartCoroutine(ChangeCinematic(Manager.manager.fadeManager.fadeTime));
    }

    IEnumerator ChangeCinematic(float time)
    {
        yield return new WaitForSeconds(time);

        if (index == 1)
        {
            PauseCinematic();

            dollyCart.m_Position = dollyCartPos;

            Manager.manager.buttonManager.FadeMainMenu();
        }
        else
        {
            DisableCinematic();
        }
    }

    //Function to disable the ability to skip
    IEnumerator DisableSkip(float time)
    {
        yield return new WaitForSeconds(time);

        canSkip = false;
    }

    IEnumerator WaitForPauseTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (!skipped)
        {
            canSkip = false;
            index++;

            PauseCinematic();
        }
    }

    //Pause/Resume/End the cinematics
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

        canSkip = true;

        StartCoroutine(DisableSkip(resumeTime - 1.5f));

        StartCoroutine(EndFirstCinematic());
    }

    IEnumerator EndFirstCinematic()
    {
        yield return new WaitForSeconds(resumeTime);

        Manager.manager.fadeManager.StartFade(null, false, null);

        yield return new WaitForSeconds(Manager.manager.fadeManager.fadeTime);

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
