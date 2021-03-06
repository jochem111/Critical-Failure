using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStarter : MonoBehaviour
{
    public enum MinigameNames {None, Drinking, Shoe, Write, Plumbing, Picture, OpenDoor }

    public int currentTimeBlock;

    public void StartNamedMinigame(MinigameNames MinigameName)
    {
        currentTimeBlock = Manager.manager.tavernManager.pointerIndex;
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
            case MinigameNames.None:
                Manager.manager.dialogueManager.AbbruptlyEndDailogue();
                break;
            case MinigameNames.OpenDoor:
                Manager.manager.tavernManager.RotateDoor(true);
                Manager.manager.dialogueManager.AbbruptlyEndDailogue();
                break;
            default:
                break;
        }
    }


    void StartDrinkingMinigame()
    {
        Manager.manager.drinkGameManager.OpenMinigame(currentTimeBlock, currentTimeBlock * 10);
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
        switch (currentTimeBlock)
        {
            case 1:
                Manager.manager.plumbingManager.OpenMinigame(0);
                break;
            case 4:
                Manager.manager.plumbingManager.OpenMinigame(1);
                break;
            case 5:
                Manager.manager.plumbingManager.OpenMinigame(2);
                break;
            case 6:
                Manager.manager.plumbingManager.OpenMinigame(3);
                break;
            default:
                break;
        }
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Plumbing;
    }

    void StartPictureMinigame()
    {
        Manager.manager.pictureManager.OnStart();
        Manager.manager.minigameStopper.currentMinigame = MinigameNames.Picture;
    }

}
