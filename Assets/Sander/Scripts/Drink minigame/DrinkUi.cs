using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrinkUi : MonoBehaviour
{
    DrinkGameManager manager;

    public GameObject timer;
    public GameObject tutorialPanel;
    public TMP_Text requestText;


    private void Awake()
    {
        manager = gameObject.GetComponent<DrinkGameManager>();
    }

    void Start()
    {
        
        if (!tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(true);
        }
    }

    void Update()
    {
        
    }

    public void RequestDrink(int index)
    {
        requestText.text = manager.drinkTypes[index];
        manager.currentRequestedDrinkIndex = index;
    }
}