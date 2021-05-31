using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMug : MonoBehaviour
{
    MugSpawner spawner;
    public float mugFillTimeInSeconds;
    public int[] currentHeldDrinkIndexes;

    int mugIndexToFill = 0;
    public bool mugIsFull;
    bool didFill = false;
     public bool isBeingHeld = false;
     public bool canBeHeld = false;

    private void Start()
    {
        spawner = FindObjectOfType<MugSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drink Tap" && !didFill)
        {
            if (!mugIsFull)
            {
                StartCoroutine(FillMug(other.GetComponent<TapScript>())); 
            }
        }
    }

    // when the mug is picked up again while not being filled stop filling
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drink Tap" && !mugIsFull)
        {
            if (!didFill)
            {
                other.GetComponent<TapScript>().fillFX.SetActive(false); // i used getComp because i felt like stashing the script would not gain much
                StopAllCoroutines();  //Used StopAll because stop didnt work
                print("FillMug stopped!");
            }
            else
            {
                didFill = false;
            }
        }
    } 

    private void OnMouseDown()
    {
        if (canBeHeld)
        {
            isBeingHeld = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnMouseDrag()
    {
        if (isBeingHeld)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawner.screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
            gameObject.transform.position = cursorPosition;
        }
    }

    private void OnMouseUp()
    {
        if (isBeingHeld)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private IEnumerator FillMug(TapScript tapScript)
    {
        //play sound
        spawner.isHoldingMug = false;
        isBeingHeld = false;
        canBeHeld = true;
        gameObject.transform.position = tapScript.fillFX.transform.position; //this can be replaced with a vec3 but this gives a good position already

        tapScript.fillFX.SetActive(true);
        yield return new WaitForSeconds(mugFillTimeInSeconds);
        tapScript.fillFX.SetActive(false);

        currentHeldDrinkIndexes[mugIndexToFill] = tapScript.drinkId;
        mugIndexToFill++;
        didFill = true;

        if (mugIndexToFill == currentHeldDrinkIndexes.Length)
        {
            mugIsFull = true;
        }
        print("FillMug done!!");
    }


}
