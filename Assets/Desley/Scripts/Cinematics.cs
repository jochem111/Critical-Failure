using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Cinematics : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] GameObject lookat;
    [SerializeField] Image blackImage;

    [Space, SerializeField] float originalCartSpeed;
    [SerializeField] float fadeTime;
    public float introTime;

    void Start()
    {
        originalCartSpeed = dollyCart.m_Speed;

        StartCoroutine(FadeFromBlack(fadeTime));
        StartCoroutine(PauseCinematic(introTime));
    }

    IEnumerator FadeFromBlack(float time)
    {
        Color alpha = blackImage.color;

        while(blackImage.color.a > 0)
         {
            alpha.a -= Time.deltaTime / time;
            blackImage.color = alpha;
            yield return null;
         }
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
        mainCamera.GetComponent<CinemachineBrain>().enabled = false;
        //activate movement
        gameObject.SetActive(false);
    }
}
