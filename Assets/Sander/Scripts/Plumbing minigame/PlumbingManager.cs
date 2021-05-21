using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumbingManager : MonoBehaviour
{
    PlumbingUI uiManager;
    public bool gameIsRunning = false;
    public int puzzleId = 0;

    [Tooltip("H = horizontal | V = vertical | U = up | D = down | R = right | L = left")]
    public Pipe[] pipes;
    public PipeScript endPipe;
    public PipeScript startPipe;

    public int currentSelectedPipeIndex = 0;
    int mouseScrollNormalized;


    private void Start()
    {
        uiManager = FindObjectOfType<PlumbingUI>();

        StartGame();
    }

    void StartGame()
    {
        uiManager.TurnOnUi();
        // turn on correct prefab
        // set startPipe/endPipe var
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
                    if (Input.GetButtonDown("Fire1"))
                    {
                        hit.transform.GetComponent<GridUnit>().PlacePipe(pipes[currentSelectedPipeIndex]);
                    }
                    else if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.GetComponent<GridUnit>().RemovePipe();
                        startPipe.ConnectToOtherPipes();
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

                if (currentSelectedPipeIndex >= pipes.Length)
                {
                    currentSelectedPipeIndex = 0;
                }
                else if (currentSelectedPipeIndex < 0)
                {
                    currentSelectedPipeIndex = pipes.Length - 1;
                }

                uiManager.UpdatedSelectedOutline(currentSelectedPipeIndex);
            }

            if (endPipe.hasWaterInPipe)
            {
                WinMinigame();
            } 
        }
    }

    void WinMinigame()
    {
        gameIsRunning = false;
        uiManager.winScreen.SetActive(true); 
    }
}
