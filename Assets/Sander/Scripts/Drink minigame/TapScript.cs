using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    public GameObject fillFX;
    public int drinkId = 1;
    //make sure this is the same as the index of the corresponding "drinkType" in the 'DrinkGameManager'
    //0 is used for empty parts in the mug and should never be used
}
