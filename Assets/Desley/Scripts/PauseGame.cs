using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMove;
    [SerializeField] CinemachineFreeLook playerCam;

    [Space, SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject areUSureMenu;
    [SerializeField] Slider slider;


    void Update()
    {
        if (Input.GetButtonDown("Cancel") && playerMove.canMove)
            OpenPauseMenu();
    }

    void OpenPauseMenu()
    {
        slider.value = Manager.manager.musicManager.gameVolume;

        Cursor.lockState = CursorLockMode.None;

        playerMove.canMove = false;
        playerMove.SetPlayerAnimation(0);

        playerCam.enabled = false;

        pauseMenu.SetActive(true); ;
    }

    public void ClosePauseMenu()
    {
        playerMove.canMove = true;
        playerCam.enabled = true;

        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AreUSure()
    {
        pauseMenu.SetActive(false);
        areUSureMenu.SetActive(true);
    }

    public void ButtonChoice(bool sure)
    {
        if (sure)
        {
            Manager.manager.starManager.StartFinishGame();
        }
        else
        {
            areUSureMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
