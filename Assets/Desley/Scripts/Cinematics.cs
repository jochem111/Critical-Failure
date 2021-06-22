using Cinemachine;
using System.Collections;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    [SerializeField] GameObject player;
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

    [Space, SerializeField] GameObject insideCinematic;

    [Space, SerializeField] GameObject outsideVcam;
    [SerializeField] Transform secondCinematicPos;
    [SerializeField] Transform tavernTeleportPos;
    [SerializeField] float cinematic2Time;
    [SerializeField] GameObject vcamReset;

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

    //Function to skip the first cinematic
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

    //Function to disable the ability to skip first cinematic
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

    //Pause/Resume/End the first cinematic
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

        if (index != 2)
        {
            Manager.manager.fadeManager.StartFade(null, false, null);

            yield return new WaitForSeconds(Manager.manager.fadeManager.fadeTime);

            DisableCinematic();
        }
    }

    public void DisableCinematic()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        skipCanvasGroup.gameObject.SetActive(false);

        playerMove.AllowMovement(true);
        insideCinematic.SetActive(false);
    }

    //Make skip text appear
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

        if (showSkipText)
            StartCoroutine(SkipFading(skipTime));
    }

    //Start second cinematic
    public void SecondCinematic() 
    {
        playerMove.canMove = false;

        player.transform.position = secondCinematicPos.position;
        player.transform.rotation = secondCinematicPos.rotation;

        outsideVcam.SetActive(true);
        playerMove.CinematicLock(true);

        StartCoroutine(TeleportToTavern());
    }

    IEnumerator TeleportToTavern()
    {
        yield return new WaitForSeconds(cinematic2Time);

        Manager.manager.fadeManager.StartFade(null, false, null);

        yield return new WaitForSeconds(Manager.manager.fadeManager.fadeTime);

        player.transform.position = tavernTeleportPos.position;
        vcamReset.SetActive(true);

        Manager.manager.tavernManager.RotateDoor(-90);
        playerMove.canMove = false;

        yield return new WaitForSeconds(.5f);

        playerMove.CinematicLock(false);
        gameObject.SetActive(false);
    }
}
