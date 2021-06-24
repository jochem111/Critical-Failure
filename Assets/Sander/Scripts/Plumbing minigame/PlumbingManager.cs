using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumbingManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsRunning = false;
    public GameObject gameVCam;
    public float orthoSize = 2f;

    public GameObject[] puzzelPrefabs;

    [Tooltip("H = horizontal | V = vertical | U = up | D = down | R = right | L = left")]
    public Pipe[] pipesToPlace;
    PlumbingTap startPipe;
    PlumbingIdScript puzzelId;

    public List<PipeScript> pipesWithWater = new List<PipeScript>();
    
    public int currentSelectedPipeIndex = 0;
    int mouseScrollNormalized;


    public void OpenMinigame(int puzzelID)
    {
        Manager.manager.fadeManager.StartFade(gameVCam, true, Manager.manager.plumbingUI.plumbingGameUi);
        foreach (GameObject puzzel in puzzelPrefabs)
        {
            puzzel.SetActive(false);
        }
        puzzelPrefabs[puzzelID].SetActive(true);
        startPipe = puzzelPrefabs[puzzelID].GetComponentInChildren<PlumbingTap>();
        puzzelId = puzzelPrefabs[puzzelID].GetComponent<PlumbingIdScript>();
        UpdatePipeAmountsForId();
        Manager.manager.plumbingUI.TurnOnUi();
    }

    public void CloseMinigame(bool didWin)
    {
        gameVCam.SetActive(false);
        print(gameVCam.activeSelf);
        Manager.manager.plumbingUI.TurnOffUi();
        Manager.manager.timer.SetTimerCamState(false);
        if (didWin)
        {
            Manager.manager.starManager.AddStar();
        }
        else
        {
            Manager.manager.starManager.FailStar();
        }
        foreach (PipeScript item in pipesWithWater)
        {
            item.RemoveSelf();
        }
        
    }

    public void StartGame()        // This is called by a button on the TutorialUI
    {
        Manager.manager.timer.StartTimer(puzzelId.MinigameTime);
        Manager.manager.plumbingUI.tutorialUi.SetActive(false);
        Manager.manager.timer.SetTimerCamState(true);
        gameIsRunning = true;
    }

    private void Update()
    {
        if (gameIsRunning)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);

                if (hit.transform.tag == "Plumbing_GridUnit")
                {
                    GridUnit gridUnit = hit.transform.GetComponent<GridUnit>();

                    if (Input.GetButtonDown("Fire1"))
                    {
                        gridUnit.PlacePipe(pipesToPlace[currentSelectedPipeIndex]);
                    }
                    else if (Input.GetButtonDown("Fire2"))
                    {
                        gridUnit.RemovePipe();
                    }

                }
                else if (hit.transform.tag == "Plumbing_PipePiece")
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.GetComponent<PipeScript>().RemoveSelf();
                    }
                }

            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                mouseScrollNormalized = Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
                currentSelectedPipeIndex -= mouseScrollNormalized;

                if (currentSelectedPipeIndex >= pipesToPlace.Length)
                {
                    currentSelectedPipeIndex = 0;
                }
                else if (currentSelectedPipeIndex < 0)
                {
                    currentSelectedPipeIndex = pipesToPlace.Length - 1;
                }

                Manager.manager.plumbingUI.UpdatedSelectedOutline(currentSelectedPipeIndex);
            }
        }
    }

    public void UpdateWaterInPipes()
    {
        foreach (var pipe in pipesWithWater)
        {
            pipe.hasWaterInPipe = false;
        }
        pipesWithWater.Clear();
        startPipe.Fill();
    }

    void UpdatePipeAmountsForId()
    {
        for (int i = 0; i < pipesToPlace.Length; i++)
        {
            pipesToPlace[i].amount = puzzelId.PipeAmounts[i];
        }
    }

    public void WinMinigame()
    {
        gameIsRunning = false;
        Manager.manager.timer.SetTimerPauzeState(true);
        Manager.manager.plumbingUI.winScreen.SetActive(true);
    }

    public void FailMinigame()
    {
        gameIsRunning = false;
        Manager.manager.plumbingUI.failScreen.SetActive(true);
    }
}
