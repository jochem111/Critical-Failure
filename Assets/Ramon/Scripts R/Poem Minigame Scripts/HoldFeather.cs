using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldFeather : MonoBehaviour
{
    private Vector3 screenPoint;

    private void Awake()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Cursor.visible = false;
    }

    private void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        transform.position = cursorPosition;
    }
}
