using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] FadeManager fadeManager;
    [SerializeField] GameObject vCam;

    [Space, SerializeField] GameObject[] stars;
    int currentStars;
    int maxStars;

    [Space, SerializeField] float transitionTime;
    [SerializeField] float intensityTime;

    void Start()
    {
        maxStars = stars.Length;
    }

    public void AddStar()
    {
        if(currentStars < maxStars)
            StartCoroutine(RevealStar(transitionTime));
    }

    IEnumerator RevealStar(float time)
    {
        vCam.SetActive(true);

        yield return new WaitForSeconds(time);

        //Up start material's intensity
        Material mat = stars[currentStars].GetComponent<Renderer>().materials[1];
        Color eColor = mat.GetColor("_EmissionColor");
        float intensity = 0;

        while(intensity < 1)
         {
            intensity += Time.deltaTime / intensityTime;

            mat.SetColor("_EmissionColor", new Vector4(intensity, intensity, 0, 0));
            yield return null;
        }

        //Play particle

        currentStars++;
        vCam.SetActive(false);

        StopCoroutine(nameof(RevealStar));
    }
}
