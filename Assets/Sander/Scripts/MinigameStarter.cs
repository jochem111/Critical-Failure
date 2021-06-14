using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStarter : MonoBehaviour
{
    public enum MinigameNames {None, Drinking, Shoe, Write, Plumbing, Picture }

    public void StartNamedMinigame(MinigameNames MinigameName)
    {
        switch (MinigameName)
        {

            case MinigameNames.Drinking:
                StartDrinkingMinigame();
                break;
            case MinigameNames.Shoe:
                StartShoeMinigame();
                break;
            case MinigameNames.Write:
                StartWriteMinigame();
                break;
            case MinigameNames.Plumbing:
                StartPlumbingMinigame();
                break;
            case MinigameNames.Picture:
                StartPictureMinigame();
                break;
            default:
                break;
        }
    }


    void StartDrinkingMinigame()
    {
        Manager.manager.drinkGameManager.OpenMinigame();
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Drinking;
    }

    void StartShoeMinigame()
    {
        Manager.manager.shoeManager.OnStart();
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Shoe;
    }

    void StartWriteMinigame()
    {
        Manager.manager.poemMinigame.OpenMinigame();
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Write;
    }

    void StartPlumbingMinigame()
    {
        Manager.manager.plumbingManager.OpenMinigame(0); // make sure to update int
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Plumbing;
    }

    void StartPictureMinigame()
    {
        print("-!- Open picture minigame -!-");
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Picture;
    }

}
