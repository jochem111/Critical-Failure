using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenChange : MonoBehaviour
{
    public Shoe shoe;
    public RotateObject shoeRotation;
    public Dirt dirt;
    public MakeDirty spawnDirt;
    public ShoeCleaningParticles particles;

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



    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        shoes.text = successCounter.ToString();
        outOf.text = successesNeededToWin.ToString();
    }

    private void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        EscapeScreen();
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
        if (Input.GetButtonDown("Cancel") && gameScreen.activeSelf)
        {
            gameScreen.SetActive(false);
            escapeScreen.SetActive(true);
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
        gameScreen.SetActive(true);

        Manager.manager.starManager.AddStar();
    }

    IEnumerator IFailScreen(int time)
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        failScreen.SetActive(false);
        gameScreen.SetActive(true);

        Manager.manager.starManager.FailStar();
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
