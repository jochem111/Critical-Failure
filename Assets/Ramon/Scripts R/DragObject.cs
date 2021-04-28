using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
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
        inSlot = false;
        inCorrectSlot = false;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        WrongAnswer();
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
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
    }

    public void WrongAnswer()
    {
        if (poemMinigame.cross == true)
        {
            if (inCorrectSlot == false)
            {
                Debug.Log("WrongAnswer");
                cross.SetActive(true);
            }
        }
    }
}
