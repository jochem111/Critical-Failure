using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectSlot : MonoBehaviour, IDropHandler
{
   [HideInInspector]public PoemMinigame poemMinigame;

    public float slotNumber;
    public float point;

    private void Start()
    {
        poemMinigame = FindObjectOfType<PoemMinigame>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            poemMinigame.answers += point;

            if (eventData.pointerDrag.GetComponent<DragObject>().wordNumber == slotNumber)
            {
                poemMinigame.correctAnswers += point;

                eventData.pointerDrag.GetComponent<DragObject>().inCorrectSlot = true;
            }

            eventData.pointerDrag.GetComponent<DragObject>().inSlot = true;
        }
    }
}
