using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool selected;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        selected = false;
    }
}
