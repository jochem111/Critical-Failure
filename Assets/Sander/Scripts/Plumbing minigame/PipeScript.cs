using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : PlumbingTap
{
    public bool hasWaterInPipe = false;
    [HideInInspector]public GridUnit myGridUnit;

    private void Start()
    {
        Manager.manager.plumbingManager.UpdateWaterInPipes();
    }

    public void RemoveSelf()
    {
        Manager.manager.plumbingManager.UpdateWaterInPipes();
        myGridUnit.RemovePipe();
    }

   


}
