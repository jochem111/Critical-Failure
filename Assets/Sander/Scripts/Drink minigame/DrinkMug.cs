using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMug : MonoBehaviour
{
    MugSpawner spawner;
    public float mugFillTimeInSeconds;
    public int[] currentHeldDrinkIndexes;

    public int mugIndexToFill = 0;
    public bool mugIsFull;
    bool didFill = false;
    bool startedFill = false;
     public bool isBeingHeld = false;
     public bool canBeHeld = false;

    private IEnumerator enumerator;
    TapScript currTap;

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
                other.GetComponent<TapScript>().CoolDownStart();
                print("start fill");
                currTap = other.GetComponent<TapScript>();
               enumerator = FillMug(other.GetComponent<TapScript>());
               StartCoroutine(enumerator);
            }
        }
    }

    // when the mug is picked up again while not being filled stop filling
   /* private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drink Tap" && !mugIsFull)
        {
            if (!didFill)
            {
                other.GetComponent<TapScript>().fillFX.SetActive(false);
                print("stopped fill");
                print(transform.position);
                StopCoroutine(enumerator);
              //  GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                didFill = false;
            }
            other.GetComponent<TapScript>().CoolDownStart();
        }
    } */

    private void OnMouseDown()
    {
        if (canBeHeld)
        {
            if (!didFill && startedFill)
            {
                currTap.fillFX.SetActive(false);
                print("picekd up stopped fill");
                print(transform.position);
                StopCoroutine(enumerator);
                startedFill = false;
            }
            else
            {
                didFill = false;
            } 

            isBeingHeld = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = false;
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
        startedFill = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;

        spawner.isHoldingMug = false;
        isBeingHeld = false;
        canBeHeld = true;
        gameObject.transform.position = tapScript.fillFX.transform.position; 

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
        startedFill = false;
        print("FillMug done!!");
    }


}
