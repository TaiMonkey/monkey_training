using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using MonkeyBase.Observer;
using System;

public class ButtonDemo1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{

    /*  [SerializeField] private Button btn;
 [SerializeField] private RectTransform rect;
 [SerializeField] private CanvasGroup canvasGroup;
 [SerializeField] public TMP_Text textContent;
 public Vector3 orgPos;
 private int indexSlibing = 0;
 private BELT01ButtonData databutton;

 public Vector3 OrignPos { get => orgPos; set { orgPos = value; } }
 public Transform pointMoved;
 private Vector3 originScale;



 void Start()
 {
     canvasGroup = GetComponent<CanvasGroup>();
     btn = GetComponent<Button>();
     rect = GetComponent<RectTransform>();
     indexSlibing = transform.GetSiblingIndex();
     //btn.onClick.AddListener(ClickItem);
     originScale = transform.localScale;
     orgPos = transform.position;

 }
 public void FadeButton(float value)
 {
     canvasGroup.DOFade(value, 0.5f);
 }

 public void OnPointerDown(PointerEventData eventData)
 {
     Debug.LogError("OnPointerDown");
     transform.SetAsLastSibling();
     transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.Linear);

 }

 public void OnPointerUp(PointerEventData eventData)
 {
     Debug.LogError("OnPointerUp");
     transform.SetSiblingIndex(indexSlibing);
     transform.DOScale(originScale, 0.5f).SetEase(Ease.Linear);
     canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.Linear);
     transform.SetAsLastSibling();
 }

 public void OnDrag(PointerEventData eventData)
 {
     Debug.LogError("OnDrag");
     RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
     rect.localPosition = localPoint;
 }

 public void OnBeginDrag(PointerEventData eventData)
 {
 }

 public void OnEndDrag(PointerEventData eventData)
 {
     Debug.LogError("OnEndDrag");
     transform.DOMove(orgPos, 0.5f);
 }
 public void InitData(BELT01ButtonData data)
 {
     databutton = data;
     textContent.text = data.text;
     textContent.ForceMeshUpdate();

 }
*/
   
    [SerializeField] public TMP_Text textContent;
    [SerializeField] private Button btn;
    private RectTransform imageDrag;
    [SerializeField] private RectTransform itemTransform;
    [SerializeField]private CanvasGroup canvasGroup;
    private Vector3 originPos;
    private bool isEnable;
    private BELT01ButtonData databutton;
    //game Drag
    private bool isDraggging = false;
    private bool isDraggable = false;



    public CanvasGroup CanvasGroup { get => canvasGroup; }
    public bool IsEnable { get => isEnable; set { isEnable = value; } }
    public Vector3 OriginPos { set { originPos = value; } }
    public BELT01ButtonData Databutton { get => databutton;  }
    

    private void Start()
    {
       
    }
 

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        if (!isDraggging)
        {
            Debug.LogError("Click");
            BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.ClickStart, this);
            ObserverManager.TriggerEvent(bELT01MCPDataChanner);
        }
        else
        {
            isDraggging = false;
            isEnable = false;
            if (CheckTriggerOfTwoObject(itemTransform, imageDrag, 0.2f))
            {
                Debug.LogError("Drag trùng khớp");
                //Game Spam
                /*                if (isCorrect)
                                {
                                    transform.DOMove(imageDrag.transform.position, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                                    {
                                        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                                        {
                                            BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.GuidingStart, null);
                                            ObserverManager.TriggerEvent(bELT01MCPDataChanner);
                                        };
                                    };
                                }
                                else
                                {
                                    transform.DOMove(originPos, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                                    {
                                        isEnable = true;
                                        BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.GuidingStart, null);
                                        ObserverManager.TriggerEvent(bELT01MCPDataChanner);
                                    };
                                }*/

                //Game khong Spam
                BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.DragResualStart, this);
                ObserverManager.TriggerEvent(bELT01MCPDataChanner);
            }
            else
            {
                Debug.LogError("bay ve");
                OnBackButton(() =>
                {
                    isEnable = true;
                    BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.GuidingStateStart, null);
                    ObserverManager.TriggerEvent(bELT01MCPDataChanner);
                });
            }
        }
        isDraggable = false;
    }
    public void OnBackButton(Action callBack)
    {
        transform.DOMove(originPos, 0.3f).SetEase(Ease.Linear).onComplete += () =>
        {
            callBack.Invoke();
        };
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(itemTransform.parent as RectTransform, eventData.position,
            eventData.pressEventCamera, out Vector2 localPoint);
        itemTransform.localPosition = localPoint;
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isEnable || !isDraggable) return;
        transform.SetAsLastSibling();
        isDraggging = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isEnable || isDraggable) return;
      
        isDraggable = true;
    }
    public void InitData(BELT01ButtonData data, RectTransform imageDrag)
    {
        databutton = data;
        textContent.text = data.text;
        textContent.ForceMeshUpdate();
        this.imageDrag = imageDrag;

    }

    //Check drag 
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
