using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private GameObject gameManager;

    public bool isWet;

    public int dirtAmountDefault;
    public int dirtAmount;
    public int cleanAmount;
    public int minimumDirtAmount;
    public int noMouseMovement;

    public float cleanTime;
    public float oncePerSecond;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        gameManager = GameObject.Find("Game Manager");
        dirtAmountDefault = dirtAmount;
    }

    public void CheckDirt()
    {
        if (dirtAmount <= minimumDirtAmount)
        {
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        if (gameManager.GetComponent<HoldTool>().firstTool.activeSelf)
        {
            isWet = true;
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void OnMouseDrag()
    {
        if (gameManager.GetComponent<HoldTool>().secondTool.activeSelf && isWet == true)
        {
            if(Input.GetAxis("Mouse X") != noMouseMovement || Input.GetAxis("Mouse Y") != noMouseMovement)
            {
                if (Time.time >= oncePerSecond)
                {
                    oncePerSecond = Time.time + cleanTime;

                    Debug.Log("Once Every Second 1");
                    dirtAmount -= cleanAmount;
                    CheckDirt();
                }
            }
        }
    }
}
