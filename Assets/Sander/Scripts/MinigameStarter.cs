using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStarter : MonoBehaviour
{
    public enum minigameNames {None, Drinking, Shoe, Write, Plumbing, Cleaning }

    public void StartNamedMinigame(minigameNames MinigameName)
    {
        switch (MinigameName)
        {

            case minigameNames.Drinking:
                StartDrinkingMinigame();
                break;
            case minigameNames.Shoe:
                StartShoeMinigame();
                break;
            case minigameNames.Write:
                StartWriteMinigame();
                break;
            case minigameNames.Plumbing:
                StartPlumbingMinigame();
                break;
            case minigameNames.Cleaning:
                StartCleaningMinigame();
                break;
            default:
                break;
        }
    }


    void StartDrinkingMinigame()
    {
        Manager.manager.drinkGameManager.OpenMinigame();
    }

    void StartShoeMinigame()
    {
        Manager.manager.shoeManager.OnStart();
    }

    void StartWriteMinigame()
    {
        Manager.manager.poemMinigame.OpenMinigame();
    }

    void StartPlumbingMinigame()
    {
        Manager.manager.plumbingManager.OpenMinigame();
    }

    void StartCleaningMinigame()
    {
        print("-!- Open Cleaning minigame -!-");
    }

}
