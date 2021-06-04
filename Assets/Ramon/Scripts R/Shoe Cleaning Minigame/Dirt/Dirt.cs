using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{

    public Color dirtColor;
    public Color lowerTransparencyBy;

    public bool isWet;

    public int dirtAmountDefault;
    public int dirtAmount;
    public int cleanAmount;
    public int minimumDirtAmount;
    public int noMouseMovement;
    public int mouseButton;

    public float cleanTime;
    public float oncePerSecond;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
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
        if (Manager.manager.holdTool.firstTool.activeSelf)
        {
            isWet = true;
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void OnMouseOver()
    {
        if (Manager.manager.holdTool.secondTool.activeSelf && isWet == true && Input.GetMouseButton(mouseButton))
        {
            if (Input.GetAxis("Mouse X") != noMouseMovement || Input.GetAxis("Mouse Y") != noMouseMovement)
            {
                if (Time.time >= oncePerSecond)
                {
                    MakeTransparent();

                    oncePerSecond = Time.time + cleanTime;

                    dirtAmount -= cleanAmount;
                    CheckDirt();
                }
            }
        }
    }

    public void MakeTransparent()
    {
        dirtColor.a -= lowerTransparencyBy.a;
        GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, dirtColor.a);
    }
}
