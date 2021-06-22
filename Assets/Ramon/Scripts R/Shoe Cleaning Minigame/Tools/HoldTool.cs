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
        OnStart();
    }

    public void OnStart()
    {
        handIsEmpty = true;
    }

    public void SelectFirstTool()
    {
        RemoveTool();

        Cursor.visible = false;

        handIsEmpty = false;

        firstTool.SetActive(true);
    }

    public void SelectSecondTool()
    {
        RemoveTool();

        Cursor.visible = false;

        handIsEmpty = false;

        secondTool.SetActive(true);
    }

    public void SelectThirdTool()
    {
        RemoveTool();

        Cursor.visible = false;

        handIsEmpty = false;

        thirdTool.SetActive(true);
    }

    public void RemoveTool()
    {
        foreach (GameObject tool in toolList)
        {
            tool.SetActive(false);
        }

        Cursor.visible = true;

        handIsEmpty = true;
    }
}
