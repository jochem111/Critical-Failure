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

    public float cleanTime;
    public float oncePerSecond;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        cleanlinessDefault = cleanliness;
    }

    private void OnMouseDrag()
    {
        if (gameManager.GetComponent<HoldTool>().thirdTool.activeSelf)
        {
            if (dirtCount <= minimumCleanliness)
            {
                if (Time.time >= oncePerSecond)
                {
                    oncePerSecond = Time.time + cleanTime;

                    Debug.Log("Once Every Second 2");
                    cleanliness -= cleanAmount;
                    CheckCleanliness();
                }
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