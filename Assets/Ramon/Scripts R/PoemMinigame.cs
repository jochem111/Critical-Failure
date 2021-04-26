using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PoemMinigame : MonoBehaviour
{
    public GameObject poem;
    public GameObject exit;
    public GameObject win;

    public TextMeshProUGUI answersList;
    public TextMeshProUGUI correctAnswersList;

    public float answers;
    public float correctAnswers;
    public float maxAnswers;

    public string returnToScene;

    void Update()
    {
        ExitPoemMinigame();

        ListAnswers();
    }

    public void CheckAnswers()
    {
        ListCorrectAnswers();

        if (correctAnswers == maxAnswers)
        {
            poem.SetActive(false);
            win.SetActive(true);
        }
    }

    public void ListCorrectAnswers()
    {
        correctAnswersList.text = correctAnswers.ToString();
    }

    public void ListAnswers()
    {
        answersList.text = answers.ToString();
    }

    public void ExitPoemMinigame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("ExitPoemMinigame");
            poem.SetActive(false);
            exit.SetActive(true);
        }
    }

    public void YesExitButton()
    {
        Debug.Log("YesExitButton");
        exit.SetActive(false);
        poem.SetActive(true);
        SceneManager.LoadScene(returnToScene);
    }

    public void NoExitButton()
    {
        Debug.Log("NoExitButton");
        exit.SetActive(false);
        poem.SetActive(true);
    }

    public void MinigameWon()
    {
        Debug.Log("MinigameWon");
        SceneManager.LoadScene(returnToScene);
    }
}