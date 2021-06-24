using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldFeather : MonoBehaviour
{
    private Vector3 screenPoint;
    [HideInInspector]public bool isAllowedToHoldFeather = false;

    public void PickUpFeather()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Cursor.visible = false;
        isAllowedToHoldFeather = true;
    }

    private void Update()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        transform.position = cursorPosition;
    }

    public void PutDownFeather()
    {
        Cursor.visible = true;
        isAllowedToHoldFeather = false;
    }

}
