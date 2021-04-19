using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSpawner : MonoBehaviour
{
    DrinkGameManager gameManager;
    public GameObject drinkMug;
    public GameObject mugSpawnPoint;

    Vector3 screenPoint;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<DrinkGameManager>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1")) //add bool to check if there is a mug
        {
            gameManager.mug.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnMouseDown()
    {
        gameManager.mug = Instantiate(drinkMug, mugSpawnPoint.transform);
        gameManager.mugScript = gameManager.mug.GetComponent<DrinkMug>();

        screenPoint = Camera.main.WorldToScreenPoint(gameManager.mug.transform.position);
    }

    private void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
        gameManager.mug.transform.position = cursorPosition;
        gameManager.mug.GetComponent<Rigidbody>().useGravity = false;
    }
}
