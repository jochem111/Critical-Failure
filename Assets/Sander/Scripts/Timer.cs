using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject clockCam;
    public GameObject timerUi;
    public Transform clockHand;
    float handSpeed;

    float timeRemaining = 60;

    [HideInInspector]
    public bool timerIsRunning = false;

    public TMP_Text timeText;


    public void SetTimerCamState(bool state)
    {
        clockCam.SetActive(state);
        timerUi.SetActive(state);
    }

    public void StartTimer(float minigameMaxTime)
    {
        timeRemaining = minigameMaxTime;
        handSpeed = 360 / minigameMaxTime;
        timerIsRunning = true;
    }

    public void SetTimerPauzeState(bool state) 
    {
        timerIsRunning = !state;
    }

    private void Update()
    {

        if (timerIsRunning == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Manager.manager.minigameStopper.FailMinigame();
                timeRemaining = 0;
                timerIsRunning = false;
            }
            MoveClockHand();
            DisplayTime(timeRemaining);
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);


        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void MoveClockHand()
    {
        clockHand.Rotate(0f , handSpeed * Time.deltaTime , 0f);
    }
}
