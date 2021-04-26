using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectSlot : MonoBehaviour, IDropHandler
{
    public PoemMinigame poemMinigame;

    public float slotNumber;
    public float point;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            poemMinigame.answers += point;

            if (eventData.pointerDrag.GetComponent<WordNumber>().wordNumber == slotNumber)
            {
                poemMinigame.correctAnswers += point;
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}
