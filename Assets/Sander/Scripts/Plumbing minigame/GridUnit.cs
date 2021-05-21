using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour
{
    public GameObject pipeSpawnPoint;
    public GameObject myPipe;
 
        //place currently selceted pipe piece if you have atleast 1
        //have pipe piece check if it is connected to other and change water status for self and others if needed

        //add way to remove pipe and update water status for all next pipes


    public void PlacePipe(Pipe pipe)
    {
        if (pipe.amount > 0)
        {
            myPipe =  Instantiate(pipe.pipePrefab, pipeSpawnPoint.transform);
            myPipe.GetComponent<PipeScript>().myGridUnit = this;
        }
    }

    public void RemovePipe()
    {
        Destroy(myPipe);
        myPipe = null;
    }
}
