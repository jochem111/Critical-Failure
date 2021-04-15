using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGameManager : MonoBehaviour
{
    DrinkUi UiManager;
    DrinkMug Mug;

    public string[] drinkTypes;
    [HideInInspector] public int currentRequestedDrinkIndex;

    public int scoreNeeded;
    public int currentScore;


    private void Awake()
    {
        Mug = gameObject.GetComponent<DrinkMug>();
        UiManager = gameObject.GetComponent<DrinkUi>();
    }

    void StartGame()
    {
        //start the timer
        UiManager.RequestDrink(Random.Range(0, drinkTypes.Length - 1));
        
    }

    void GiveDrink()
    {
        if (Mug.currentHeldDrinkIndex == currentRequestedDrinkIndex)
        {
            currentScore++;
            if (currentScore == scoreNeeded)
            {
                //win 
            }
            //update score, play happy sound & request new drink

        }
        else
        {
            //play mad sound & request new drink
        }
    }
}
