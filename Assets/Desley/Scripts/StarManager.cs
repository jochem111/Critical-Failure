using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StarManager : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer playerRenderer;
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] CinemachineFreeLook playerCam;
    [SerializeField] GameObject vCam;
    [SerializeField] Interact interact;

    [Space, SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] stars2;
    int currentStars;

    [Space, SerializeField] float transitionTime;
    [SerializeField] float intensityTime;
    [SerializeField] float soundTime;

    [Space] public GameObject loseScreen;

    public void AddStar()
    {
        if (currentStars < stars.Length)
            StartCoroutine(RevealStar(true));
        else
            interact.FinishInteraction();
    }

    public void FailStar()
    {
        StartCoroutine(RevealStar(false));
    }

    IEnumerator RevealStar(bool won)
    {
        Manager.manager.musicManager.MinigameTrack(false);

        //Turn off player meshRenderer
        PlayerVisible(false);

        vCam.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        if (won)
        {
            //Get intensity of material
            Material mat = stars[currentStars].GetComponent<Renderer>().materials[1];
            Color eColor = mat.GetColor("_EmissionColor");
            float intensity = 0;

            //Up intensity of tavern1 star
            while (intensity < .5f)
            {
                intensity += Time.deltaTime / intensityTime;

                mat.SetColor("_EmissionColor", new Vector4(intensity, intensity, 0, 0));
                yield return null;
            }

            //Up intensity of tavern2 star
            mat = stars2[currentStars].GetComponent<Renderer>().materials[1];
            eColor = mat.GetColor("_EmissionColor");
            mat.SetColor("_EmissionColor", new Vector4(intensity, intensity, 0, 0));

            //Sound effect
            Manager.manager.musicManager.StarSound();

            currentStars++;
        }

        yield return new WaitForSeconds(soundTime);

        //Finish game if all stars are filled
        if (currentStars == stars.Length)
        {
            StartCoroutine(FinishGame());
            yield break;
        }

        Manager.manager.musicManager.InTavernTrack(true);

        //Open tavern door
        Manager.manager.tavernManager.RotateDoor(true);

        vCam.SetActive(false);

        //Make player visible again
        PlayerVisible(true);

        interact.FinishInteraction();

        StopCoroutine(nameof(RevealStar));
    }

    void PlayerVisible(bool active)
    {
        playerRenderer.enabled = active;
    }

    public void RevealLossScreen()
    {
        playerMove.canMove = false;
        playerMove.SetPlayerAnimation(0);

        playerCam.enabled = false;

        loseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartFinishGame() { StartCoroutine(FinishGame()); }

    IEnumerator FinishGame()
    {
        Manager.manager.musicManager.InTavernTrack(false);

        loseScreen.SetActive(false);

        Manager.manager.fadeManager.StartFade(null, false, null);

        yield return new WaitForSeconds(Manager.manager.fadeManager.fadeTime);

        SceneManager.LoadScene(0);
    }
}
