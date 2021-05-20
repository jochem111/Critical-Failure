using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    [SerializeField] Transform door;

    public void RotateDoor(float rotation)
    {
        door.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
