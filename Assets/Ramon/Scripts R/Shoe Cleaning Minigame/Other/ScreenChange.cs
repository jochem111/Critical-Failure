using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public Shoe shoe;
    public Dirt dirt;
    public MakeDirty spawnDirt;

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

    public string returnToScene;

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
        shoes.text = successCounter.ToString();

        if (successesNeededToWin <= successCounter)
        {
            WinScreen();
        }
        else
        {
            NextScreen();
        }
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

    IEnumerator IWinScreen(int time)
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        winScreen.SetActive(false);
        gameScreen.SetActive(true);

        SceneManager.LoadScene(returnToScene);
    }

    IEnumerator IFailScreen(int time)
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        failScreen.SetActive(false);
        gameScreen.SetActive(true);

        SceneManager.LoadScene(returnToScene);
    }

    public void NextScreen()
    {
        if (increasedDifficulty == true)
        {
            shoe.cleanlinessDefault += difficultyIncrease;
            dirt.dirtAmountDefault += difficultyIncrease;
            spawnDirt.dirtCount += difficultyExtraDirt;
        }

        gameScreen.SetActive(false);
        nextScreen.SetActive(true);
    }

    public void NextScreenButton()
    {
        nextScreen.SetActive(false);

        shoe.cleanliness = shoe.cleanlinessDefault;
        spawnDirt.SpawnObjects(spawnDirt.dirtCount);

        gameScreen.SetActive(true);
    }
}
