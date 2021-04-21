using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTrigger : MonoBehaviour
{
    public string tagName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagName)
        {
            Destroy(other.gameObject);
        }
    }
}
