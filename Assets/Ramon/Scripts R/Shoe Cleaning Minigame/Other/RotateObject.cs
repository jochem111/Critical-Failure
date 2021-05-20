using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public HoldTool tool;

    private Vector3 setAngularVelocity;

    private Quaternion defaultRotation;

    public int mouseClick;
    public float rotationSpeed;
    private bool dragging;

    private void Start()
    {
        dragging = false;
        defaultRotation = transform.rotation;
    }

    private void OnMouseDrag()
    {
        if (tool.handIsEmpty == true)
        {
            dragging = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(mouseClick))
        {
            dragging = false;
            
        }

        if (dragging)
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -rotX, Space.World);
            transform.Rotate(Vector3.right, rotY, Space.World);
        }
    }

    public void ResetPosition()
    {
        transform.rotation = defaultRotation;
    }
}
