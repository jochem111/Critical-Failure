using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMug : MonoBehaviour
{
    public float mugFillTimeInSeconds;

    public int[] currentHeldDrinkIndexes;

    int mugIndexToFill = 0;
    public bool mugIsFull;
    bool didFill = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drink Tap" && !didFill)
        {
            if (!mugIsFull)
            {
                StartCoroutine(FillMug(other.GetComponent<TapScript>().drinkId)); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drink Tap" && !mugIsFull)
        {
            if (!didFill)
            {
                StopAllCoroutines();  //Used StopAll because stop didnt work
                print("FillMug stopped!");
            }
            else
            {
                didFill = false;
            }
        }
    }

    private IEnumerator FillMug(int drinkIndexFromTap)
    {
        //play sound & particles

        yield return new WaitForSeconds(mugFillTimeInSeconds);

        currentHeldDrinkIndexes[mugIndexToFill] = drinkIndexFromTap;
        mugIndexToFill++;
        didFill = true;

        if (mugIndexToFill == currentHeldDrinkIndexes.Length)
        {
            mugIsFull = true;
        }
        print("FillMug done!!");
    }

    

}
