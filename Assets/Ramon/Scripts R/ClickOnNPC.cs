using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnNPC : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI text;

    public bool panelIsActive;
    public bool canDoPoem;
    public bool hasAlreadyTalkedTo;

    void Start()
    {
        panelIsActive = false;
        canDoPoem = false;
        hasAlreadyTalkedTo = false;
    }

    void Update()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (panelIsActive == true)
            {
                panel.SetActive(!panel.activeSelf);
                panelIsActive = false;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.gameObject.tag == "PoemMinigameNPC")
                    {
                        if (hasAlreadyTalkedTo == true)
                        {
                            text.SetText("We already talked, go do the minigame.");
                        }

                        panel.SetActive(!panel.activeSelf);
                        panelIsActive = true;
                        canDoPoem = true;
                        hasAlreadyTalkedTo = true;

                        print("Poem NPC Clicked");
                    }
                }
            }
        }
    }
}
