using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugDropTrigger : MonoBehaviour
{
    public GameObject dropFx;
    public Vector3 offset;

    DrinkGameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<DrinkGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrinkMug")
        {
            Instantiate(dropFx, other.transform.position - offset, Quaternion.identity);
            gameManager.currentAmountDroppedMugs++;
            Destroy(other.gameObject);
        }

    }
}
