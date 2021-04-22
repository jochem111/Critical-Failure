using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PoemMinigame : MonoBehaviour
{
    public ClickOnNPC npc;
    public GameObject poem;
    public bool poemIsActive;

    void Start()
    {
        poemIsActive = false;  
    }

    void Update()
    {
        MinigameActivate();
    }

    void MinigameActivate()
    {
        if (npc.canDoPoem == true)
        {
            CanDoPoem();
            DoingPoem();
            QuitPoem();
        }
    }

    void CanDoPoem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "Poem")
                {
                    print("Book Clicked");
                    npc.panel.SetActive(!npc.panel.activeSelf);
                    npc.panelIsActive = false;
                    poem.SetActive(!poem.activeSelf);
                    poemIsActive = true;
                }
            }
        }
    }

    void DoingPoem()
    {
        if(poemIsActive == true)
        {
            //do stuff
        }
    }

    void QuitPoem()
    {
        if (poemIsActive == true && Input.GetButtonDown("Cancel"))
        {
            print("Minigame Quit");
            poem.SetActive(!poem.activeSelf);
            poemIsActive = false;
        }
    }
}
