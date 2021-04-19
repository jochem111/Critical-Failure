using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMug : MonoBehaviour
{
    public float mugFillTimeInSeconds;

    public int[] currentHeldDrinkIndexes;

    int mugIndexToFill = 0;
    public bool mugIsFull;


    private void Start()
    {
        //StartCoroutine(FillMug(50));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drink Tap")
        {
            if (!mugIsFull)
            {
                StartCoroutine(FillMug(1)); // other.<tapscript>.drinkindex instead of 1
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines(); //Used StopAll because stop didnt work
        print("FillMug stopped!");
    }

    private IEnumerator FillMug(int drinkIndexFromTap)
    {
        //play sound & particles

        yield return new WaitForSeconds(mugFillTimeInSeconds);

        currentHeldDrinkIndexes[mugIndexToFill] = drinkIndexFromTap;
        mugIndexToFill++;
        if (mugIndexToFill == currentHeldDrinkIndexes.Length)
        {
            mugIsFull = true;
        }
        print("FillMug done!!");
    }

    

}
