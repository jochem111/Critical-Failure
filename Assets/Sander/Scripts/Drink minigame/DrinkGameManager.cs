using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGameManager : MonoBehaviour
{
    [HideInInspector]public DrinkMug mugScript;
    MugSpawner mugSpawner;
    public Transform droppedMugSpawnPoint;
    public GameObject gameVCam;

    [HideInInspector]public bool gameIsRunning = false;

    //always make sure that if lets say index "1" is 'Blue' then that the drink ID on the 'Blue' keg also "1" is  
    public string[] drinkTypes;

    //make sure that this is the same size as the DrinkMug "currentHeldDrinkIndexes"
    public int[] currentRequestedDrinkIndexes;

    public int maxScore;
    [HideInInspector] public int currentScore;

    //this is currently just used for spawning some mugs on the ground after the minigame as a little gag
    [HideInInspector] public int currentAmountDroppedMugs = 0;
    [HideInInspector] public int maxAmountDroppedMugs = 5; 


    private void Awake()
    {
        mugSpawner = FindObjectOfType<MugSpawner>();
    }

    public void OpenMinigame()
    {
        Manager.manager.fadeManager.StartFade(gameVCam, true, Manager.manager.drinkUi.drinkGameUi);
        Manager.manager.drinkUi.SetScoreTextToZeroOutOfMax();
        currentScore = 0;
        currentAmountDroppedMugs = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMinigame(bool didWin)
    {
        // spawn the dropped mugs
        Manager.manager.drinkUi.winScreen.SetActive(false);
        Manager.manager.drinkUi.drinkGameUi.SetActive(false);
        if (didWin)
        {
            Manager.manager.starManager.AddStar();
        }
        Manager.manager.starManager.FailStar();
    }

    public void StartGame()
    {
        //start the timer
        gameIsRunning = true;
        Manager.manager.drinkUi.tutorialUi.SetActive(false);
        RequestDrink();
    }

    public void WinMinigame()
    {
        //stop timer
        Manager.manager.drinkUi.winScreen.SetActive(true);
        gameIsRunning = false;
    }

    public void FailMinigame()
    {
        // timer already stopped cuz this gets called on time out?
        CloseMinigame(false);
    }

    public void RequestDrink()
    {
        for (int i = 0; i < currentRequestedDrinkIndexes.Length; i++)
        {
            int requestID = Random.Range(1, drinkTypes.Length);
            currentRequestedDrinkIndexes[i] = requestID;
            Manager.manager.drinkUi.UpdateRequest(i, drinkTypes[requestID]);
        }
    }

    private bool ArraysAreTheSame()
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
                Manager.manager.drinkUi.UpdateScore(currentScore);
                //play happy sound

                if (currentScore == maxScore)
                {
                    WinMinigame();
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
