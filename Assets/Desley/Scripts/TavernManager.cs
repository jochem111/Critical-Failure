using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    [SerializeField] Interact playerInteract;
    [SerializeField] GameObject tavern1Trigger, tavern2Trigger;

    [Space, SerializeField] Transform door;
    [SerializeField] float originalY;

    bool inTavern1;

    //Customer shit
    [SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] tavern1Customers, tavern2Customers;
    GameObject extraCustomer = null;
    int roundIndex;

    //Clock shit
    [Space, SerializeField] Transform hPointer1, hPointer2;
    [SerializeField] float[] pointerRotation;
    int pointerIndex;

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

        //Deactivate all customers in other tavern
        if (!inTavern1)
            foreach(GameObject obj in tavern1Customers) { obj.SetActive(false); }
        else
            foreach(GameObject obj in tavern2Customers) { obj.SetActive(false); }

        //Activate needed customers in other tavern
        if(roundIndex < customers.Length)
        {
            customers[roundIndex].SetActive(true);
            customers[roundIndex + 1].SetActive(true);

            roundIndex += 2;
        }

        //GetInteractables in interact script
        playerInteract.GetInteractables();

        //Update the clock
        if(pointerIndex < pointerRotation.Length)
        {
            hPointer1.rotation = Quaternion.Euler(pointerRotation[pointerIndex], 0, -90);
            hPointer2.rotation = Quaternion.Euler(pointerRotation[pointerIndex], 180, -90);

            pointerIndex++;
        }
    }

    public void AddExtraCustomer(GameObject customer)
    {
        extraCustomer = customer;
    }
}
