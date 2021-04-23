using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PoemMinigameStart : MonoBehaviour
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
                    print("Minigame Started");
                    SceneManager.LoadScene("Poem_Minigame");
                }
            }
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
