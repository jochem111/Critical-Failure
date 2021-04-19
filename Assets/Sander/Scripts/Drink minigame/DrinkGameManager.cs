using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGameManager : MonoBehaviour
{
    DrinkUi UiManager;
    public DrinkMug mugScript;
    public GameObject mug;

    //always make sure that if lets say index "1" is 'Blue' then that the drink ID on the 'Blue' keg also "1" is  
    public string[] drinkTypes;

    //make sure that this is the same size as the DrinkMug "currentHeldDrinkIndexes"
    public int[] currentRequestedDrinkIndexes;

    public int scoreNeeded;
    public int currentScore;

    public int currentAmountDroppedMugs = 0;
    public int maxAmountDroppedMugs = 5; //this is currently just used for spawning some mugs on the ground after the minigame as a little gag


    private void Awake()
    {
        UiManager = gameObject.GetComponent<DrinkUi>();

        //RequestDrink();
    }

    void StartGame()
    {
        //start the timer & request drink
        RequestDrink();
    }

    public void RequestDrink()
    {
        for (int i = 0; i < currentRequestedDrinkIndexes.Length; i++)
        {
            int requestID = Random.Range(1, drinkTypes.Length);
            currentRequestedDrinkIndexes[i] = requestID;
            //update Ui to show what is requested
        }
    }

    void GiveDrink()
    {
        if (mugScript.mugIsFull)
        {
            if (mugScript.currentHeldDrinkIndexes == currentRequestedDrinkIndexes)
            {
                currentScore++;
                //update score, play happy sound

                if (currentScore == scoreNeeded)
                {
                    //stop timer & no new drink request
                    UiManager.winScreen.SetActive(true);
                }
                else
                {
                    RequestDrink();
                }

            }
            else
            {
                //play mad sound
                RequestDrink();
            }
        }
        else
        {
            //play 'oh, no drink? sound' 
        }

        
    }
}
