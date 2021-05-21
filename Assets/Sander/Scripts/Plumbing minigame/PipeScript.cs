using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public bool hasWaterInPipe = false;
    [HideInInspector]public GridUnit myGridUnit;

    public GameObject rayOutPoint_1;
    public GameObject rayOutPoint_2;
    public float raycastLenght = 1f;
    RaycastHit hit_1;
    RaycastHit hit_2;

    public bool isStartPipe = false;
    public bool isEndPipe = false;

    private void Start()
    {
        //check connection and water status with other pipes and update accordingly

        if (!isStartPipe || !isEndPipe)
        {
            ConnectToOtherPipes();
        }

    }

    public void DisconnectedFromFilledPipes()
    {
        hasWaterInPipe = false; // call this function on the next pipe
        // do the same as in connect?
    }

    public void RemoveSelf()
    {
        if (!isStartPipe || !isEndPipe)
        {
            DisconnectedFromFilledPipes();
            myGridUnit.RemovePipe();
        }

    }

    public void ConnectToOtherPipes()
    {
        Physics.Raycast(rayOutPoint_1.transform.position, rayOutPoint_1.transform.forward, out hit_1, raycastLenght);
        Physics.Raycast(rayOutPoint_2.transform.position, rayOutPoint_2.transform.forward, out hit_2, raycastLenght);


        // heel scuffed dit
        if (hit_1.transform != null)
        { 
            if (hit_1.transform.tag == "Plumbing_PipePiece")
            {
                if (hit_1.transform.GetComponent<PipeScript>().hasWaterInPipe)
                {
                    hasWaterInPipe = true;
                    if (hit_2.transform != null)
                    {
                        if (hit_2.transform.tag == "Plumbing_PipePiece") 
                        {
                            hit_2.transform.GetComponent<PipeScript>().hasWaterInPipe = true;
                        }
                    }
                }
            }
        }
        if (hit_2.transform != null)
        {
            if (hit_2.transform.tag == "Plumbing_PipePiece")
            {
                if (hit_2.transform.GetComponent<PipeScript>().hasWaterInPipe)
                {
                    hasWaterInPipe = true;
                    if (hit_1.transform != null)
                    {
                        if (hit_1.transform.tag == "Plumbing_PipePiece") 
                        {
                            hit_1.transform.GetComponent<PipeScript>().hasWaterInPipe = true;
                        }
                    }

                }

            }
        }

    }


}
