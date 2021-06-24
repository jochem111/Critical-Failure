using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public GameObject cross;

    public PoemMinigame poemMinigame;

    public float point;
    public float wordNumber;

    public bool inSlot;
    public bool inCorrectSlot;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        inSlot = false;
        inCorrectSlot = false;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;

        if (inSlot == true)
        {
            poemMinigame.answers -= point;

            if (inCorrectSlot == true)
            {
                poemMinigame.correctAnswers -= point;
            }

            inSlot = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / 2.2f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
    }

    public void WrongAnswer()
    {
        if (inCorrectSlot == false)
        {
            Debug.Log("WrongAnswer");
            cross.SetActive(true);
        }
    }
}
