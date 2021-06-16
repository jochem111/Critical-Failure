using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : PlumbingTap
{
    public int pipeIndex;
    [HideInInspector]public bool hasWaterInPipe = false;
    [HideInInspector]public GridUnit myGridUnit;

    private void Start()
    {
        Manager.manager.plumbingManager.UpdateWaterInPipes();
    }

    public void RemoveSelf()
    {
        myGridUnit.RemovePipe();
    }

   


}
