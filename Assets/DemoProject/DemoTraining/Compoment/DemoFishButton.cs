using DG.Tweening;
using MonkeyBase.Observer;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemoFishButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{

    private RectTransform imageDrag;
    [SerializeField] private RectTransform itemTransfrom;
    [SerializeField] private CanvasGroup canvasGroup;
    private Vector3 originPos;
    private bool isEnable;

    //game drag
    private bool isDragging = false;
    private bool isDraggable = false;

    public CanvasGroup CanvasGroup { get => canvasGroup; }
    public bool IsEnable { get => isEnable; set { isEnable = value; } }
    public Vector3 OriginPos { set { originPos = value; } }

    public bool isCorrect;

   

    public void InitData(RectTransform imageDrag)
    {
        this.imageDrag = imageDrag;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isEnable || isDraggable) return;

        isDraggable = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        if (!isDragging)
        {
            Debug.LogError("Click");
            Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.ClickStart, this);
            ObserverManager.TriggerEvent(buoi3Channer);
        }
        else
        {
            isDragging = false;
            isEnable = false;
            if (CheckTriggerOfTwoObject(itemTransfrom, imageDrag, 0.2f))
            {
                Debug.LogError("Drag trùng khớp");
                //Game spam
                /*  if (isCorrect)
                  {
                      transform.DOMove(imageDrag.transform.position, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                      {
                          transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                          {
                              Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.GuidingStart, null);
                              ObserverManager.TriggerEvent(buoi3Channer);
                          };
                      };

                  }
                  else
                  {
                      transform.DOMove(originPos, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                      {
                          isEnable = true;
                          Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.GuidingStart, null);
                          ObserverManager.TriggerEvent(buoi3Channer);
                      };
                  }*/

                //Game k spam
                Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.DragResultStart, this);
                ObserverManager.TriggerEvent(buoi3Channer);
            }
            else
            {
                Debug.LogError("Bay về");
                OnBackButton(() => {
                    isEnable = true;
                    Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.GuidingStart, null);
                    ObserverManager.TriggerEvent(buoi3Channer);
                });
            }
        }
        isDraggable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(itemTransfrom.parent as RectTransform, 
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        itemTransfrom.localPosition = localPoint;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        transform.SetAsLastSibling();
        Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.DragginggStart, this);
        ObserverManager.TriggerEvent(buoi3Channer);
        isDragging = true;

    }

    public void OnBackButton(Action callBack)
    {
        transform.DOMove(originPos, 0.3f).SetEase(Ease.Linear).onComplete += () =>
        {
            callBack.Invoke();
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
