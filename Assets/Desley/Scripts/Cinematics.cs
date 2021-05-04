using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Cinematics : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] GameObject lookat;
    [SerializeField] GameObject mainMenu, settingsMenu;

    [Space, SerializeField] float originalCartSpeed;
    public float introTime;

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
        lookat.GetComponent<MoveLookat>().enabled = true;
    }

    public void DisableCinematic()
    {
        playerMove.AllowMovement(true);
        gameObject.SetActive(false);

        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
}
