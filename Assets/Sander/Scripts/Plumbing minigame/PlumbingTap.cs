using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumbingTap : MonoBehaviour
{
    public GameObject[] rayOutPoints;
    public float raycastLenght = 1f;
    RaycastHit hit;
    PipeScript targetPipeScript;
    public float MinigameTime;

    public void Fill()
    {
        foreach (var point in rayOutPoints)
        {
            // the only objects it should be able to hit are other pipes or the endpipe
            if (Physics.Raycast(point.transform.position, point.transform.forward, out hit, raycastLenght))
            {
                if (hit.transform.GetComponent<PipeScript>())
                {
                    targetPipeScript = hit.transform.GetComponent<PipeScript>();

                    if (!targetPipeScript.hasWaterInPipe)
                    {
                        Manager.manager.plumbingManager.pipesWithWater.Add(targetPipeScript);
                        targetPipeScript.hasWaterInPipe = true;
                        targetPipeScript.Fill();
                    }

                }
                else if (hit.transform.tag == "Plumbing_PipeEnd")
                {
                    Manager.manager.plumbingManager.WinMinigame();
                }
            }
        }



    }
}
