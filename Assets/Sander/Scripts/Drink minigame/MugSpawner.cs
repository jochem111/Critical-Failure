using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSpawner : MonoBehaviour
{
    DrinkGameManager gameManager;
    public GameObject drinkMug;
    [HideInInspector] public GameObject currentMug;
    public bool isHoldingMug = false;

    [Tooltip("spawnpoint is mainly used for the right Z")]
    public GameObject mugSpawnPoint;


    Vector3 screenPoint;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<DrinkGameManager>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1") && currentMug != null) 
        {
            currentMug.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnMouseDown()
    {
        if (gameManager.gameHasStarted)
        {
            currentMug = Instantiate(drinkMug, mugSpawnPoint.transform);
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
}
