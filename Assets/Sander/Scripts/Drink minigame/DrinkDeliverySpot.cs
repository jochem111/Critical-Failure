using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkDeliverySpot : MonoBehaviour
{
    DrinkGameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<DrinkGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrinkMug")
        {
            gameManager.GiveDrink();
        }
    }
}
