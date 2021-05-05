using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrinkUi : MonoBehaviour
{
    DrinkGameManager gameManager;

    public GameObject timer;
    public GameObject drinkGameUi;
    public GameObject winScreen;
    public TMP_Text scoreText;
    public TMP_Text[] requestTexts;


    private void Awake()
    {
        gameManager = gameObject.GetComponent<DrinkGameManager>();
        scoreText.text = "0 / " + gameManager.maxScore.ToString();
    }

    public void TurnOnUi()
    {
        //turn on all but win Ui
        
    }

    public void TurnOffUi()
    {
        //turn off All drink game Ui
    }

    public void UpdateRequest(int index, string drinkType)
    {
        requestTexts[index].text = drinkType;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString() + " / " + gameManager.maxScore.ToString();
    }

}
