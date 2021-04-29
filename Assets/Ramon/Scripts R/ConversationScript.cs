using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationScript : MonoBehaviour
{


    //Negeer deze chunk maar dit is tijdelijk om te testen of het werkt.
    public string startButton;
    private void Update()
    {
        if (Input.GetButtonDown(startButton))
        {
            StartConversation();
        }
    }
    //Vanaf hier is code die niet tijdelijk is.

    public void StartConversation()
    {

    }
}
