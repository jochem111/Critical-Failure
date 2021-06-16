using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlumbingUI : MonoBehaviour
{
    public GameObject plumbingGameUi;
    public GameObject tutorialUi;
    public GameObject winScreen;
    public GameObject failScreen;
    public TMP_Text[] pipeAmountTexts;
    public Outline[] pipeSelectedOutlines;


    public void TurnOnUi()
    {
        tutorialUi.SetActive(true);

        UpdatePipeAmountTexts();

        UpdatedSelectedOutline(0);
    }

    public void TurnOffUi()
    {
        plumbingGameUi.SetActive(false);
        tutorialUi.SetActive(false);
        winScreen.SetActive(false);
        failScreen.SetActive(false);

    } 

    public void UpdatePipeAmountTexts()
    {
        for (int i = 0; i < pipeAmountTexts.Length; i++)
        {
            pipeAmountTexts[i].text = Manager.manager.plumbingManager.pipesToPlace[i].amount.ToString();
        }
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
