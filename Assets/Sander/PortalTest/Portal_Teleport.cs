using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Teleport : MonoBehaviour
{
    public GameObject tavern_2;


    // Start is called before the first frame update
    void Start()
    {
        tavern_2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        tavern_2.SetActive(true);
        // tp player
    }
}
