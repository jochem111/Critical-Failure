using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGameManager : MonoBehaviour
{
    DrinkUi uiManager;
    [HideInInspector]public DrinkMug mugScript;
    MugSpawner mugSpawner;

    [HideInInspector]public bool gameHasStarted = false;

    //always make sure that if lets say index "1" is 'Blue' then that the drink ID on the 'Blue' keg also "1" is  
    public string[] drinkTypes;

    //make sure that this is the same size as the DrinkMug "currentHeldDrinkIndexes"
    public int[] currentRequestedDrinkIndexes;

    public int maxScore;
    public int currentScore;

    //this is currently just used for spawning some mugs on the ground after the minigame as a little gag
    public int currentAmountDroppedMugs = 0;
    public int maxAmountDroppedMugs = 5; 


    private void Awake()
    {
        uiManager = gameObject.GetComponent<DrinkUi>();
        mugSpawner = FindObjectOfType<MugSpawner>();
    }

    public void StartGame()
    {
        //start the timer
        gameHasStarted = true;
        uiManager.tutorialPanel.SetActive(false);
        RequestDrink();
    }

    public void RequestDrink()
    {
        for (int i = 0; i < currentRequestedDrinkIndexes.Length; i++)
        {
            int requestID = Random.Range(1, drinkTypes.Length);
            currentRequestedDrinkIndexes[i] = requestID;
            uiManager.UpdateRequest(i, drinkTypes[requestID]);
        }
    }

    private bool ArraysAreTheSame ()
    {
        if (mugScript.currentHeldDrinkIndexes.Length != currentRequestedDrinkIndexes.Length) return false;
        for (int i = 0; i < currentRequestedDrinkIndexes.Length; i++)
        {
            if (mugScript.currentHeldDrinkIndexes[i] != currentRequestedDrinkIndexes[i]) return false;
        }
        return true;
    }

    public void GiveDrink()
    {
        if (mugScript.mugIsFull)
        {
            if (ArraysAreTheSame())
            {
                currentScore++;
                uiManager.UpdateScore(currentScore);
                //play happy sound

                if (currentScore == maxScore)
                {
                    //stop timer
                    uiManager.winScreen.SetActive(true);
                    gameHasStarted = false;
                }
                else
                {
                    RequestDrink();
                    print("Good drink, next!");
                }

            }
            else
            {
                //play mad sound 
                print("Wrong drink");
                RequestDrink();
            }

            mugSpawner.isHoldingMug = false;
            Destroy(mugScript.gameObject);
        }
        else
        {
            print("oh no drink?");
            //play 'oh, no drink? sound' 
        }

        
    }
}
