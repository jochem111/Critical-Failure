using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    public GameObject gameManager;

    public int cleanlinessDefault;
    public int cleanliness;
    public int minimumCleanliness;
    public int dirtCount;
    public int cleanAmount;
    public int successCounterIncrease;

    private void Start()
    {
        cleanlinessDefault = cleanliness;
    }

    private void OnMouseDown()
    {
        if (gameManager.GetComponent<HoldTool>().thirdTool.activeSelf)
        {
            if (dirtCount <= minimumCleanliness)
            {
                cleanliness -= cleanAmount;
                CheckCleanliness();
            }
        }
    }

    public void CheckCleanliness()
    {
        if (cleanliness <= minimumCleanliness)
        {
            gameManager.GetComponent<ScreenChange>().successCounter += successCounterIncrease;
            gameManager.GetComponent<ScreenChange>().ChangeScreen();
        }
    }
}