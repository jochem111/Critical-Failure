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
    public GameObject lose;
    public GameObject checkAnswers;
    public GameObject areYouSure;

    public TextMeshProUGUI answersList;
    public TextMeshProUGUI correctAnswersList;
    public TextMeshProUGUI youNeededMore;

    public List<GameObject> list;

    public float answers;
    public float correctAnswers;
    public float answersRequired;
    public float pauseLength;
    public float youNeeded;

    public string returnToScene;

    void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        ExitPoemMinigame();

        ListAnswers();
    }

    public void CheckAnswers()
    {
        checkAnswers.SetActive(false);
        areYouSure.SetActive(true);
    }

    public void CheckAnswersYes()
    {
        ListCorrectAnswers();
        areYouSure.SetActive(false);
        foreach (GameObject dragObject in list)
        {
            dragObject.GetComponent<DragObject>().WrongAnswer();
        }
        StartCoroutine(Pause(pauseLength));
        youNeeded = answersRequired - correctAnswers;
        youNeededMore.text = youNeeded.ToString();
    }

    public void CheckAnswersNo()
    {
        areYouSure.SetActive(false);
        checkAnswers.SetActive(true);
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
        if (Input.GetButtonDown("Cancel") && poem.activeSelf)
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

    public void MinigameLost()
    {
        Debug.Log("MinigameLost");
        SceneManager.LoadScene(returnToScene);
    }

    IEnumerator Pause(float time)
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        poem.SetActive(false);
        if (correctAnswers >= answersRequired)
        {
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
        }
    }
}