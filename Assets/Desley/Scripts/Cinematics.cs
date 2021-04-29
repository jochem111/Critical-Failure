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

    [Space, SerializeField] float originalCartSpeed;
    public float introTime;

    void Start()
    {
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
        playerMove.enabled = true;
        gameObject.SetActive(false);
    }
}
