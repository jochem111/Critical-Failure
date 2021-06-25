using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : MonoBehaviour
{
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
        if (Manager.manager.shoeManager.GetComponent<HoldTool>().thirdTool.activeSelf && Input.GetMouseButton(mouseButton))
        {
            Debug.Log("Shoe OnMouseOver 1");

            if (dirtCount <= minimumCleanliness)
            {
                Debug.Log("Shoe OnMouseOver 2");

                if (Time.time >= oncePerSecond)
                {
                    Debug.Log("Shoe OnMouseOver 3");

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
                Manager.manager.shoeManager.GetComponent<ScreenChange>().successCounter += successCounterIncrease;
                Manager.manager.shoeManager.GetComponent<ScreenChange>().ChangeScreen();
            }
        }
    }
}