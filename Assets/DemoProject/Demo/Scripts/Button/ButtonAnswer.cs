using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnswer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    private RectTransform buttonDrag;
    private RectTransform buttonTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originPos;
    private bool isEnable;

    //Game drag
    private bool isDragging = false;
    private bool isDraggable = false;

    public CanvasGroup CanvasGroup { get => canvasGroup; }
    public bool IsEnable { get => isEnable; set { isEnable = value; } }
    public Vector3 OriginPos { set { originPos = value; } }
    public bool isCorrect;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        buttonTransform = GetComponent<RectTransform>();
    }

    public void InitData(RectTransform buttonDrag)
    {
        this.buttonDrag = buttonDrag;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        transform.SetAsLastSibling();
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isEnable || !isDragging) return;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(buttonTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        buttonTransform.localPosition = localPoint;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isEnable || isDraggable) return;
        isDraggable = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        if(!isDragging)
        {
            Debug.LogError("Click");
            DemoChannel demoChannel = new DemoChannel(DemoConstValue.ClickStart, this);
            ObserverManager.TriggerEvent(demoChannel);
        } 
        else
        {
            isDragging = false;
            isEnable = false;
            if(CheckTriggerOfTwoObject(buttonTransform, buttonDrag, 0.2f)) {
                Debug.LogError("Drag trung khop");
                DemoChannel demoChannel = new DemoChannel(DemoConstValue.DragStart, this);
                ObserverManager.TriggerEvent(demoChannel);
            }
            else
            {
                Debug.LogError("Bay ve");
                OnBackButton(() =>
                {
                    IsEnable = true;
                    DemoChannel demoChannel = new DemoChannel(DemoConstValue.GuidingStart, null);
                    ObserverManager.TriggerEvent(demoChannel);
                });
            }
        }
        isDraggable = false;
    }

    public void OnBackButton(Action callback)
    {
        transform.DOMove(originPos, 0.2f).SetEase(Ease.Linear).onComplete += () =>
        {
            callback.Invoke();
        };
    }
    private bool CheckTriggerOfTwoObject(RectTransform objectA, RectTransform objectB, float percent)
    {
        Vector3[] objectACorners = new Vector3[4];
        Vector3[] objectBCorners = new Vector3[4];

        objectA.GetWorldCorners(objectACorners);
        objectB.GetWorldCorners(objectBCorners);

        Rect rectA = new(objectACorners[0].x, objectACorners[0].y, objectACorners[2].x - objectACorners[0].x, objectACorners[2].y - objectACorners[0].y);

        Rect rectB = new(objectBCorners[0].x, objectBCorners[0].y, objectBCorners[2].x - objectBCorners[0].x, objectBCorners[2].y - objectBCorners[0].y);

        Rect intersection = Rect.MinMaxRect(
            Mathf.Max(rectA.xMin, rectB.xMin),
            Mathf.Max(rectA.yMin, rectB.yMin),
            Mathf.Min(rectA.xMax, rectB.xMax),
            Mathf.Min(rectA.yMax, rectB.yMax)
        );

        float intersectionArea = Mathf.Max(0, intersection.width) * Mathf.Max(0, intersection.height);
        float rectAArea = rectA.width * rectA.height;
        float rectBArea = rectB.width * rectB.height;

        float overlapPercentageA = (intersectionArea / rectAArea) * 100f;
        float overlapPercentageB = (intersectionArea / rectBArea) * 100f;

        return overlapPercentageA >= percent * 100f && overlapPercentageB >= percent * 100f;
    }
}
