using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenChange : MonoBehaviour
{
    public GameObject gameVCam;         // Camera for the game view

    public Shoe shoe;
    public RotateObject shoeRotation;
    public Dirt dirt;
    public Dirt[,] dirtList;
    public MakeDirty spawnDirt;
    public ShoeCleaningParticles particles;
    public HoldTool tools;

    public GameObject minigameHolder;       // Added so i can make sure that all the UI is off when the minigame is not being played
    public GameObject gameScreen;
    public GameObject winScreen;
    public GameObject escapeScreen;
    public GameObject failScreen;
    public GameObject nextScreen;

    public TextMeshProUGUI shoes;       
    public TextMeshProUGUI outOf;       

    public bool increasedDifficulty;

    public int difficultyIncrease;
    public int difficultyExtraDirt;
    public int successesNeededToWin;
    public int successCounter;
    public int waitTime;

    private int returnToZero;


    public void OnStart()       // Gets called when the MinigameStarter is needs to start this minigame
    {
        ResetGame();

        Manager.manager.fadeManager.StartFade(gameVCam, true, minigameHolder);
        Cursor.lockState = CursorLockMode.None;

        shoes.text = successCounter.ToString();
        outOf.text = successesNeededToWin.ToString();

        gameScreen.SetActive(true);
        Manager.manager.timer.SetTimerCamState(true);
        Manager.manager.timer.StartTimer(90);
    }

    private void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        EscapeScreen();
    }

    public void ResetGame()
    {
        shoeRotation.ResetPosition();
        dirtList = new Dirt[spawnDirt.dirtCount, spawnDirt.dirtCount];
        successCounter = returnToZero;
        shoe.cleanliness = shoe.cleanlinessDefault;
        tools.RemoveTool();

        foreach (Dirt dirtBlock in dirtList)
        {
            dirtBlock.ResetDirt();
        }
    }

    public void ChangeScreen()
    {
        StartCoroutine(IChangeScreen(waitTime));
    }

    public void WinScreen()
    {
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
        StartCoroutine(IWinScreen(waitTime));
    }

    public void EscapeScreen()
    {
        if (Input.GetButtonDown("Cancel") && gameScreen.activeInHierarchy)
        {
            gameScreen.SetActive(false);
            escapeScreen.SetActive(true);
            Manager.manager.timer.SetTimerPauzeState(true);
        }
    }

    public void EscapeScreenY()
    {
        
        escapeScreen.SetActive(false);
        failScreen.SetActive(true);
        StartCoroutine(IFailScreen(waitTime));
    }

    public void EscapeScreenN()
    {
        Manager.manager.timer.SetTimerPauzeState(false);
        escapeScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public IEnumerator IChangeScreen(int time)
    {
        shoe.isCleaned = true;
        shoes.text = successCounter.ToString();
        particles.TriggerParticles();

        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        if (successesNeededToWin <= successCounter)
        {
            WinScreen();
        }
        else
        {
            NextScreen();
        }
    }

    IEnumerator IWinScreen(int time)        
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        winScreen.SetActive(false);
        minigameHolder.SetActive(false);        //gameScreen.SetActive(true); -> minigameHolder.SetActive(false); as when the minigame gets opened the OnStart fucntion makes sure that the "gameScreen" is on and this makes sure that ALL the UI is off after the minigame is done

        Manager.manager.starManager.AddStar();
        Manager.manager.timer.SetTimerCamState(false);
        gameVCam.SetActive(false);
    }

    IEnumerator IFailScreen(int time)
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        failScreen.SetActive(false);
        minigameHolder.SetActive(false);

        Manager.manager.starManager.FailStar();
        Manager.manager.timer.SetTimerCamState(false);
        gameVCam.SetActive(false);
    }

    public void NextScreen()
    {
        if (increasedDifficulty == true)
        {
            shoe.cleanlinessDefault += difficultyIncrease;
            dirt.dirtAmountDefault += difficultyIncrease;
            spawnDirt.dirtCount += difficultyExtraDirt;
        }

        shoeRotation.ResetPosition();

        gameScreen.SetActive(false);
        nextScreen.SetActive(true);
    }

    public void NextScreenButton()
    {
        nextScreen.SetActive(false);

        shoe.cleanliness = shoe.cleanlinessDefault;
        spawnDirt.SpawnObjects(spawnDirt.dirtCount);
        shoe.isCleaned = false;

        gameScreen.SetActive(true);
    }
}
