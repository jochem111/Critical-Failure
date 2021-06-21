using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStopper : MonoBehaviour
{
    [HideInInspector]
    public MinigameStarter.MinigameNames currentMinigame;

    public void FailMinigame()
    {
        switch (currentMinigame)
        {
            case MinigameStarter.MinigameNames.None:
                break;
            case MinigameStarter.MinigameNames.Drinking:
                FailDrinkingMinigame();
                break;
            case MinigameStarter.MinigameNames.Shoe:
                FailShoeMinigame();
                break;
            case MinigameStarter.MinigameNames.Write:
                FailWriteMinigame();
                break;
            case MinigameStarter.MinigameNames.Plumbing:
                FailPlumbingMinigame();
                break;
            case MinigameStarter.MinigameNames.Picture:
                FailPictureMinigame();
                break;
            default:
                break;
        }
    }

    void FailDrinkingMinigame()
    {
        Manager.manager.drinkGameManager.FailMinigame();
    }

    void FailShoeMinigame()
    {
        Manager.manager.shoeManager.EscapeScreenY();
    }

    void FailWriteMinigame()
    {
        Manager.manager.poemMinigame.CheckAnswersYes();
    }

    void FailPlumbingMinigame()
    {
        Manager.manager.plumbingManager.FailMinigame();
    }

    void FailPictureMinigame()
    {
        Manager.manager.pictureManager.ExitGameYes();
    }

}
