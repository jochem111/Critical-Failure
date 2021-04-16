using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float horizontal;
    public float vertical;
    public float targetAngle;
    public float angle;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    public Vector3 direction;
    public Vector3 moveDirection;
    public Vector3 newPosition;

    public Transform cam;

    void Update()
    {
        MovementInput();
    }

    public void MovementInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += (moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
    }
}
