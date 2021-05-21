using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlumbingUI : MonoBehaviour
{
    PlumbingManager gameManager;

    public GameObject plumbingGameUi;
    public GameObject timer;
    public GameObject tutorialUi;
    public GameObject winScreen;
    public TMP_Text[] pipeAmountTexts;
    public Outline[] pipeSelectedOutlines;


    public void TurnOnUi()
    {
        gameManager = FindObjectOfType<PlumbingManager>();

        for (int i = 0; i < pipeAmountTexts.Length; i++)
        {
            pipeAmountTexts[i].text = gameManager.pipes[i].amount.ToString();
        }

        UpdatedSelectedOutline(0);
        plumbingGameUi.SetActive(true);
    }

    public void TurnOffUi()
    {
        plumbingGameUi.SetActive(false);
    } 

    public void UpdatedSelectedOutline(int index)
    {
        foreach (var item in pipeSelectedOutlines)
        {
            item.enabled = false;

        }
        pipeSelectedOutlines[index].enabled = true;
    }
}
