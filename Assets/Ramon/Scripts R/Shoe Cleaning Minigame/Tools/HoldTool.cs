using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoldTool : MonoBehaviour
{
    public GameObject firstTool;
    public GameObject secondTool;
    public GameObject thirdTool;
    public GameObject[] toolList;

    public bool handIsEmpty;

    private void Start()
    {
        handIsEmpty = true;
    }

    public void SelectFirstTool()
    {
        RemoveTool();

        handIsEmpty = false;

        firstTool.SetActive(true);
    }

    public void SelectSecondTool()
    {
        RemoveTool();

        handIsEmpty = false;

        secondTool.SetActive(true);
    }

    public void SelectThirdTool()
    {
        RemoveTool();

        handIsEmpty = false;

        thirdTool.SetActive(true);
    }

    public void RemoveTool()
    {
        foreach (GameObject tool in toolList)
        {
            tool.SetActive(false);
        }

        handIsEmpty = true;
    }
}
