using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class ButtonDemo1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Button btn;
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
    
}
