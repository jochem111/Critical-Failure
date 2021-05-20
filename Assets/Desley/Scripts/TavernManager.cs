using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    [SerializeField] GameObject tavern1Trigger, tavern2Trigger;

    [Space, SerializeField] Transform door;
    [SerializeField] float originalY;

    bool inTavern1 = true;

    public void RotateDoor(float rotation)
    {
        door.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void UpdateTaverns()
    {
        RotateDoor(originalY);

        inTavern1 = !inTavern1;

        tavern1Trigger.SetActive(!inTavern1);
        tavern2Trigger.SetActive(inTavern1);

        //Activate new customers

        //Change time on clock
    }
}
