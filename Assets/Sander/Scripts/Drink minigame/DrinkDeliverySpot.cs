using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkDeliverySpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrinkMug")
        {
            Manager.manager.drinkGameManager.GiveDrink();
        }
    }
}
