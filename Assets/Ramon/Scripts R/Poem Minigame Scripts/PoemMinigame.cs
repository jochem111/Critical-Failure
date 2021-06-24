using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PoemMinigame : MonoBehaviour
{
    public GameObject gameVCam;
    public GameObject poemUiHolder;

    public GameObject poem;
    public GameObject exit;
    public GameObject win;
    public GameObject lose;
    public GameObject checkAnswers;
    public GameObject areYouSure;

    public GameObject feather;
    public HoldFeather holdFeather;

    public TextMeshProUGUI answersList;
    public TextMeshProUGUI correctAnswersList;
    public TextMeshProUGUI youNeededMore;

    public List<GameObject> list;

    public float answers;
    public float correctAnswers;
    public float answersRequired;
    [Tooltip("In Seconds")] public float pauseLength;
    public float youNeeded;


    void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        ExitPoemMinigame();

        ListAnswers();
    }

    public void OpenMinigame() // add overload for the right poem prefab so you can change which one to open
    {
        poem.SetActive(true);
        feather.SetActive(true);
        Manager.manager.fadeManager.StartFade(gameVCam, true, poemUiHolder);
        Cursor.lockState = CursorLockMode.None;
        holdFeather.PickUpFeather();

    }

    public void CheckAnswers()
    {
        checkAnswers.SetActive(false);
        areYouSure.SetActive(true);
    }

    public void CheckAnswersYes() // make sure that the player cant move their answer from this point on. Maybe we also want it so the player cant open the Exit menu from this point as they will get a leave button anyway
    {
        ListCorrectAnswers();
        areYouSure.SetActive(false);
        foreach (GameObject dragObject in list)
        {
            dragObject.GetComponent<DragObject>().ColorAnswer();
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

    public void ListAnswers() //should only be called when a answer is put in a word slot. You could use Manager.manager.poemMinigame.ListAnswers whenever you drag an item into a slot
    {
        answersList.text = answers.ToString();
    }

    public void ExitPoemMinigame()
    {
        if (Input.GetButtonDown("Cancel") && poem.activeInHierarchy)
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
        //poem.SetActive(true);

        holdFeather.PutDownFeather();
        poemUiHolder.SetActive(false);
        Manager.manager.starManager.FailStar();
        gameVCam.SetActive(false);
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

        win.SetActive(false);
        poemUiHolder.SetActive(false);
        feather.SetActive(false);
        holdFeather.PutDownFeather();
        Manager.manager.starManager.AddStar();
        gameVCam.SetActive(false);
    }

    public void MinigameLost()
    {
        Debug.Log("MinigameLost");

        lose.SetActive(false);
        poemUiHolder.SetActive(false);
        feather.SetActive(false);
        holdFeather.PutDownFeather();
        Manager.manager.starManager.FailStar();
        gameVCam.SetActive(false);
    }

    IEnumerator Pause(float time)
    {
        Debug.Log("Pause Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Pause End");

        //poem.SetActive(false); i turned this off so that we can lower the wait time and as such we can let the player leave quicker while still giving the chance to look at their answers
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