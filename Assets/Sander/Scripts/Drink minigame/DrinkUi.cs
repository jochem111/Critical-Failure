using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrinkUi : MonoBehaviour
{
    public GameObject drinkGameUi;
    public GameObject timer;
    public GameObject tutorialUi;
    public GameObject winScreen;
    public TMP_Text scoreText;
    public TMP_Text[] requestTexts;

    public void TurnOffUi()
    {
        drinkGameUi.SetActive(false);
    }

    public void UpdateRequest(int index, string drinkType)
    {
        requestTexts[index].text = drinkType;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString() + " / " + Manager.manager.drinkGameManager.maxScore.ToString();
    }

    public void SetScoreTextToZeroOutOfMax()
    {
        scoreText.text = "0 / " + Manager.manager.drinkGameManager.maxScore.ToString();
    }
}
