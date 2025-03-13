using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Spine.Unity;

public class DemoButtonBuoi3 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private DemoTrainingButtonConfig buttonConfig;
    [SerializeField] private Image imgBackground;
    [SerializeField] private Image imgShadow;
    [SerializeField] private SkeletonGraphic fishGraphic;
    [SerializeField, SpineAnimation] private string fishIdie;
    [SerializeField, SpineAnimation] private string fishTap;
    [SerializeField, SpineAnimation] private string fishUntap;
    [SerializeField, SpineAnimation] private string fishUntapLoop;

    private Button button;
    private RectTransform rect;
    private Vector3 originPos;
    private int indexSlibing = 0;
    public Vector3 OriginPos { get => originPos; set { originPos = value; } }

    public Transform pointMoved;
    private Vector3 originScale;
    private CanvasGroup canvasGroup;
    [SerializeField] private bool isCorrect;
    private const string FISH_IDLE = "Idie";
    private const string FISH_TAP = "user tap";
    private const string FISH_UN_TAP = "user Untap";
    private const string FISH_TAP_LOOP = "user Untap loop";

    private DemoButtonData dataButton;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        button = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
        indexSlibing = transform.GetSiblingIndex();
        //button.onClick.AddListener(DestroyButtonClick);
        originScale = transform.localScale;
        // transform.localScale = Vector3.zero;
        //StartCoroutine(TestDO());

        //TestDO();
        fishGraphic.AnimationState.SetAnimation(0, FISH_IDLE, true);


    }
    public void InitData(DemoButtonData data)
    {
        dataButton = data;
    }


    private void TestDO()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(pointMoved.position, 1f).SetEase(Ease.Linear));
        sequence.Join(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetEase(Ease.Linear));
        sequence.OnComplete(() =>
        {
            //originScale = transform.localScale;
        });

        //transform.DOMove(pointMoved.position, 1f).SetEase(Ease.Linear)




      /*  bool isMoveDone = false;
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f).SetEase(Ease.Linear).onComplete += () => {
            isMoveDone = true;
        };
        yield return new WaitUntil(() => isMoveDone);

        transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear).onComplete += () => {
        };*/
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogError("OnPointerDown");
        transform.SetAsLastSibling();
        fishGraphic.AnimationState.SetAnimation(0, FISH_TAP, false).Complete += (trackEntry) => {
            fishGraphic.AnimationState.SetAnimation(0, FISH_TAP_LOOP, true);
        };


        // transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.5f).SetEase(Ease.Linear);
        if (isCorrect)
        {
            imgBackground.color = buttonConfig.colorCorrect.backround;
            imgShadow.color = buttonConfig.colorCorrect.shadow;
        }else
        {
            imgBackground.color = buttonConfig.colorWrong.backround;
            imgShadow.color = buttonConfig.colorWrong.shadow;
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogError("OnPointerUp");
        transform.SetSiblingIndex(indexSlibing);
       // transform.DOScale(originScale, 0.5f).SetEase(Ease.Linear);
       // canvasGroup.DOFade(0.5f, 0.5f).SetEase(Ease.Linear);
        imgBackground.color = buttonConfig.colorNormal.backround;
        imgShadow.color = buttonConfig.colorNormal.shadow;

        fishGraphic.AnimationState.SetAnimation(0, FISH_UN_TAP, false).Complete += (trackEntry) => {
            fishGraphic.AnimationState.SetAnimation(0, FISH_IDLE, true);
        };
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.LogError("OnDrag");
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent as RectTransform, 
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        rect.localPosition = localPoint;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.LogError("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.LogError("OnEndDrag");
        transform.DOMove(originPos, 0.5f);
    }

    private void DestroyButtonClick()
    {
        Debug.LogError("Click");
        Destroy(gameObject, 2f);
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
