using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // wijzer rotatie = 360/60
    // rotatie sec wijzer + wijzer rot 
    // wacht sec en doe opnieuw
    // na 60 sec de het zelfde voor de min wijzer

    public Transform clockHand;
    float handSpeed;

    float timeRemaining = 60;

    [HideInInspector]
    public bool timerIsRunning = false;

    public TMP_Text timeText;

    private void Start() // for debug only
    {
        SetTimerState(true, 90);
    }

    public void SetTimerState(bool state, float minigameMaxTime) 
    {
        timeRemaining = minigameMaxTime;
        handSpeed = 360 / minigameMaxTime;
        timerIsRunning = state;
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
