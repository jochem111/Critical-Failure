using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinematics : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] GameObject mainMenu, settingsMenu;

    float originalCartSpeed;

    public float introTime;
    public float resumeTime;

    void Start()
    {
        playerMove.AllowMovement(false);

        originalCartSpeed = dollyCart.m_Speed;

        StartCoroutine(PauseCinematic(introTime));
    }

    IEnumerator PauseCinematic(float time)
    {
        yield return new WaitForSeconds(time);

        dollyCart.m_Speed = 0;
    }

    public void ResumeCinematic() 
    { 
        dollyCart.m_Speed = originalCartSpeed;

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
