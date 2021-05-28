using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumbingManager : MonoBehaviour
{
    public bool gameIsRunning = false;

    public int puzzleId = 0;

    [Tooltip("H = horizontal | V = vertical | U = up | D = down | R = right | L = left")]
    public Pipe[] pipesToPlace;
    public PlumbingTap startPipe;

    public List<PipeScript> pipesWithWater = new List<PipeScript>();
    
    public int currentSelectedPipeIndex = 0;
    int mouseScrollNormalized;


    public void OpenMinigame()
    {
        //  Manager.manager.plumbingUI.TurnOnUi();
    }

    void StartGame()
    {
        //allow placing pipes and turn on timer
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

    public void WinMinigame()
    {
        print("wow win");
        gameIsRunning = false;
        Manager.manager.plumbingUI.winScreen.SetActive(true); 
    }
}
