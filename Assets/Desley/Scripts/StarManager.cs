using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] MeshRenderer[] playerRenderer;
    [SerializeField] FadeManager fadeManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject vCam;
    [SerializeField] Interact interact;

    [Space, SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] stars2;
    int currentStars;

    [Space, SerializeField] float transitionTime;
    [SerializeField] float intensityTime;
    [SerializeField] float soundTime;


    public void AddStar()
    {
        if (currentStars < stars.Length)
            StartCoroutine(RevealStar());
        else
            interact.FinishInteraction();
    }

    IEnumerator RevealStar()
    {
        PlayerVisibility(false);

        vCam.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        //Get intensity of material
        Material mat = stars[currentStars].GetComponent<Renderer>().materials[1];
        Color eColor = mat.GetColor("_EmissionColor");
        float intensity = 0;

        //Up intensity of tavern1 star
        while(intensity < .5f)
         {
            intensity += Time.deltaTime / intensityTime;

            mat.SetColor("_EmissionColor", new Vector4(intensity, intensity, 0, 0));
            yield return null;
        }

        //Up intensity of tavern2 star
        mat = stars2[currentStars].GetComponent<Renderer>().materials[1];
        eColor = mat.GetColor("_EmissionColor");
        mat.SetColor("_EmissionColor", new Vector4(intensity, intensity, 0, 0));

        audioSource.Play();

        yield return new WaitForSeconds(soundTime);

        currentStars++;
        vCam.SetActive(false);

        PlayerVisibility(true);

        interact.FinishInteraction();

        StopCoroutine(nameof(RevealStar));
    }

    void PlayerVisibility(bool active)
    {
        foreach (MeshRenderer mRenderer in playerRenderer)
        {
            mRenderer.enabled = active;
        }
    }
}
