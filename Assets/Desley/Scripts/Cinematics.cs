using Cinemachine;
using System.Collections;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] GameObject mainMenu, settingsMenu;

    float originalCartSpeed;

    public float introTime;
    public float resumeTime;

    bool skipped;

    void Start()
    {
        playerMove.AllowMovement(false);

        originalCartSpeed = dollyCart.m_Speed;

        StartCoroutine(DisableSkip());
        StartCoroutine(WaitForPauseTime(introTime));
    }

    IEnumerator DisableSkip()
    {
        yield return new WaitForSeconds(introTime - 1);

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

        skipped = true;

        StopCoroutine(nameof(WaitForPauseTime));
    }

    public void ResumeCinematic() 
    { 
        dollyCart.m_Speed = originalCartSpeed;

        Manager.manager.interact.canSkip = true;

        StartCoroutine(EndCinematic(resumeTime));
    }

    IEnumerator EndCinematic(float time)
    {
        yield return new WaitForSeconds(time);

        DisableCinematic();
    }

    public void DisableCinematic()
    {
        playerMove.AllowMovement(true);
        gameObject.SetActive(false);

        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
}
