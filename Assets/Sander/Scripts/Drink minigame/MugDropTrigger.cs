using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugDropTrigger : MonoBehaviour
{
    public GameObject dropFx;
    public Vector3 offset;

    private void Start()
    {
        Manager.manager.drinkGameManager = FindObjectOfType<DrinkGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrinkMug")
        {
            Instantiate(dropFx, other.transform.position - offset, Quaternion.identity);
            Manager.manager.drinkGameManager.currentAmountDroppedMugs++;
            Destroy(other.gameObject);
        }

    }
}
