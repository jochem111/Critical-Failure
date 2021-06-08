using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    
    // this is the doorframe the player can walk into
    public Transform portalEnter;

    // this is the doorframe that the portalCam needs to look at
    public Transform portalExit;


    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - portalEnter.position;
        transform.position = portalExit.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portalExit.rotation, portalEnter.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
