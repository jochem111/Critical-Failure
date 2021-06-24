using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    [SerializeField] Interact playerInteract;

    [SerializeField] GameObject tavern1Trigger, tavern2Trigger;

    [Space, SerializeField] GameObject tavern2;
    [SerializeField] GameObject tavern1Bartender, tavern2Bartender;

    [Space, SerializeField] Transform door;

    bool inTavern1;

    //Customer shit
    [Space, SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] tavern1Customers, tavern2Customers;
    GameObject extraCustomer = null;
    int roundIndex;

    //Clock shit
    [Space, SerializeField] Transform hPointer1;
    [SerializeField] Transform hPointer2;
    [SerializeField] float[] pointerRotation;
    public int pointerIndex = -1;

    bool makeInteractable = false;

    public void RotateDoor(bool open)
    {
        float rotation = open ? 0 : -90;

        door.rotation = Quaternion.Euler(0, rotation, 0);

        door.GetComponent<Collider>().enabled = open ? false : true;
    }

    public void UpdateTaverns()
    {
        //Close door
        RotateDoor(false);
        
        //Update location bool
        inTavern1 = !inTavern1;

        //Update the tavern triggers
        tavern1Trigger.SetActive(!inTavern1);
        tavern2Trigger.SetActive(inTavern1);

        if (!tavern2.activeSelf)
        {
        tavern2.SetActive(true);
        tavern2Bartender.SetActive(true);
        }

        //Deactivate bartenders after their sequences
        if (tavern2Bartender.activeSelf && !inTavern1)
            tavern1Bartender.SetActive(false);
        else if (!tavern1Bartender.activeSelf && inTavern1)
            tavern2Bartender.SetActive(false);

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

            if (makeInteractable)
            {
                customers[roundIndex].tag = "Interactable";
                customers[roundIndex + 1].tag = "Interactable";
            }

            roundIndex += 2;

            if (extraCustomer)
            {
                extraCustomer.SetActive(true);

                extraCustomer = null;
            }
        }

        //GetInteractables in interact script
        playerInteract.GetInteractables();

        //Update the clock/equal to length? lost the game
        if (pointerIndex < pointerRotation.Length - 1)
        {
            pointerIndex++;

            hPointer1.rotation = Quaternion.Euler(pointerRotation[pointerIndex], 0, -90);
            hPointer2.rotation = Quaternion.Euler(pointerRotation[pointerIndex], 180, -90);

            if(pointerIndex == pointerRotation.Length - 1)
                Manager.manager.starManager.StartFinishGame();
        }
    }

    public void AddExtraCustomer(GameObject customer)
    {
        extraCustomer = customer;
    }

    public void EnableInteractOnCustomers()
    {
        customers[0].tag = "Interactable";
        customers[1].tag = "Interactable";

        makeInteractable = true;

        playerInteract.GetInteractables();
    }
}
