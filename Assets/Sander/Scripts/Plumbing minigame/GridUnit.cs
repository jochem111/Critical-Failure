using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour
{
    public GameObject pipeSpawnPoint;
    public GameObject myPipe;

    public void PlacePipe(Pipe pipe)
    {
        if (pipe.amount > 0 && myPipe == null)
        {
            pipe.amount--;
            myPipe =  Instantiate(pipe.pipePrefab, pipeSpawnPoint.transform);
            myPipe.GetComponent<PipeScript>().myGridUnit = this;
            Manager.manager.plumbingUI.UpdatePipeAmountTexts();
        }
    }

    public void RemovePipe()
    {
        Manager.manager.plumbingManager.pipesToPlace[myPipe.GetComponent<PipeScript>().pipeIndex].amount++;
        Manager.manager.plumbingManager.UpdateWaterInPipes();
        Manager.manager.plumbingUI.UpdatePipeAmountTexts();
        Destroy(myPipe);
        myPipe = null;
    }
}
