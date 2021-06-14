using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlumbingUI : MonoBehaviour
{
    public GameObject plumbingGameUi;
    public GameObject timer;
    public GameObject tutorialUi;
    public GameObject winScreen;
    public TMP_Text[] pipeAmountTexts;
    public Outline[] pipeSelectedOutlines;


    public void TurnOnUi()
    {
        tutorialUi.SetActive(true);

        for (int i = 0; i < pipeAmountTexts.Length; i++)
        {
            pipeAmountTexts[i].text = Manager.manager.plumbingManager.pipesToPlace[i].amount.ToString();
        }

        UpdatedSelectedOutline(0);
        //plumbingGameUi.SetActive(true);  fademanager takes care of this
    }

    public void TurnOffUi()
    {
        plumbingGameUi.SetActive(false);
        tutorialUi.SetActive(false);
        winScreen.SetActive(false);

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
