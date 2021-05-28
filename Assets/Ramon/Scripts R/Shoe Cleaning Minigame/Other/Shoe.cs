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
    public int mouseButton;

    public float cleanTime;
    public float oncePerSecond;

    public bool isCleaned;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        cleanlinessDefault = cleanliness;
        isCleaned = false;
    }

    private void OnMouseOver()
    {
        if (gameManager.GetComponent<HoldTool>().thirdTool.activeSelf && Input.GetMouseButton(mouseButton))
        {
            if (dirtCount <= minimumCleanliness)
            {
                if (Time.time >= oncePerSecond)
                {
                    oncePerSecond = Time.time + cleanTime;

                    cleanliness -= cleanAmount;
                    CheckCleanliness();
                }
            }
        }
    }

    public void CheckCleanliness()
    {
        if (isCleaned == false)
        {
            if (cleanliness <= minimumCleanliness)
            {
                gameManager.GetComponent<ScreenChange>().successCounter += successCounterIncrease;
                gameManager.GetComponent<ScreenChange>().ChangeScreen();
            }
        }
    }
}