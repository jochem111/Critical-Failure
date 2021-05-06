using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSpawner : MonoBehaviour
{
    DrinkGameManager gameManager;
    public GameObject drinkMug;
    [HideInInspector] public GameObject currentMug;

    // this bool is used to tell the spawner that it is no longer holding the mug because of an external interaction (could be giving the mug to the hand)
    [HideInInspector]public bool isHoldingMug = false;

    [Tooltip("spawnpoint is mainly used for the right worldspace Z")]
    public GameObject mugSpawnPoint;


    public Vector3 screenPoint;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<DrinkGameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager.gameIsRunning)
        {
            currentMug = Instantiate(drinkMug, mugSpawnPoint.transform.position, Quaternion.identity);
            gameManager.mugScript = currentMug.GetComponent<DrinkMug>();
            screenPoint = Camera.main.WorldToScreenPoint(currentMug.transform.position);
            isHoldingMug = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isHoldingMug)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
            currentMug.transform.position = cursorPosition;
        }
    }

    private void OnMouseUp()
    {
        if (isHoldingMug)
        {
            currentMug.GetComponent<Rigidbody>().useGravity = true;
            currentMug.GetComponent<DrinkMug>().canBeHeld = true;
        }

    }
}
