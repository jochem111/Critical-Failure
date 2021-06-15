using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockmovetest : MonoBehaviour
{
    private const float realSecondsPerFullRotation = 60f;

    public Transform clockHand;
    private float fullRot;

    private void Update()
    {

    }

    void MoveClockHand()
    {
        fullRot += Time.deltaTime / realSecondsPerFullRotation;

        float fullRotNormalized = fullRot % 1f;

        float rotationDegreesPerFullRot = 360f;
        clockHand.eulerAngles = new Vector3(0, 0, fullRotNormalized * rotationDegreesPerFullRot);
    }
}
