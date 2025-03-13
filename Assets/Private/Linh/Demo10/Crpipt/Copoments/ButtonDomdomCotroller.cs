using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Monkey.Game.CF
{
    public class ButtonDomdomCotroller : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button btn;
        [SerializeField] private RectTransform itemTrans;
        [SerializeField] private RectTransform jar1Trans;
        [SerializeField] private RectTransform jar2Trans;
        [SerializeField] private Transform itroPos;
        private CanvasGroup canvasGroup;
        private Vector3 orgPos;
        private Vector3 posOutSceen;
        private Tween move = null;
        private CFButtonDomDomData databutton;
       
        private int indexSlibing = 0;
        
        public int IsCorrectID { get; set; }

        [SerializeField] public TMP_Text textContent;

        private void Start()
        {

        }

        public void DomDomMove()
        {
            move = transform.DOLocalMove(itroPos.localPosition, 3f).SetEase(Ease.OutQuart).OnComplete(() => { });
        }
        public void InitData(CFButtonDomDomData data)
        {
            databutton = data;
            textContent.text = data.text;
            textContent.ForceMeshUpdate();

        }
        public void SetOriginLocalPosition()
        {
            orgPos = itroPos.position;
        }
        public void SetPositonOutScreen()
        {
            posOutSceen = itemTrans.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
           
        }
        public void OnBackButton(Action callBack)
        {
            transform.DOMove(orgPos, 0.3f).SetEase(Ease.Linear).onComplete += () =>
            {
                callBack.Invoke();
            };
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SoundChannel sound = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, databutton.audioClip);
            ObserverManager.TriggerEvent<SoundChannel>(sound);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(itemTrans.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
            itemTrans.localPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (CheckTriggerOfTwoObject(itemTrans, jar1Trans, jar2Trans, 0.2f))
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.PlayStart);
                ObserverManager.TriggerEvent(stateChanel);
            }
            else
            {
                OnBackButton(() => { });
            }
        }
        private bool CheckTriggerOfTwoObject(RectTransform objectA, RectTransform objectB, RectTransform objectC, float percent)
        {
            Vector3[] objectACorners = new Vector3[4];
            Vector3[] objectBCorners = new Vector3[4];
            Vector3[] objectCCorners = new Vector3[4];

            objectA.GetWorldCorners(objectACorners);
            objectB.GetWorldCorners(objectBCorners);
            objectC.GetWorldCorners(objectBCorners);

            Rect rectA = new(objectACorners[0].x, objectACorners[0].y, objectACorners[2].x - objectACorners[0].x, objectACorners[2].y - objectACorners[0].y);

            Rect rectB = new(objectBCorners[0].x, objectBCorners[0].y, objectBCorners[2].x - objectBCorners[0].x, objectBCorners[2].y - objectBCorners[0].y);

            Rect rectC = new(objectBCorners[0].x, objectBCorners[0].y, objectBCorners[2].x - objectBCorners[0].x, objectBCorners[2].y - objectBCorners[0].y);

            Rect intersection = Rect.MinMaxRect(
                Mathf.Max(rectA.xMin, rectB.xMin),
                Mathf.Max(rectA.yMin, rectB.yMin),
                Mathf.Min(rectA.xMax, rectB.xMax),
                Mathf.Min(rectA.yMax, rectB.yMax)
            ); 

            Rect intersection1 = Rect.MinMaxRect(
               Mathf.Max(rectA.xMin, rectC.xMin),
               Mathf.Max(rectA.yMin, rectC.yMin),
               Mathf.Min(rectA.xMax, rectC.xMax),
               Mathf.Min(rectA.yMax, rectC.yMax)
           );

            float intersectionArea = Mathf.Max(0, intersection.width) * Mathf.Max(0, intersection.height);
            float rectAArea = rectA.width * rectA.height;
            float rectBArea = rectB.width * rectB.height;

            float overlapPercentageA = (intersectionArea / rectAArea) * 100f;
            //float overlapPercentageB = (intersectionArea / rectBArea) * 100f;

            return overlapPercentageA >= percent * 100f;



            float intersectionArea1 = Mathf.Max(0, intersection1.width) * Mathf.Max(0, intersection1.height);
            float rectAArea1 = rectA.width * rectA.height;
            float rectCArea = rectC.width * rectC.height;

            float overlapPercentageA1 = (intersectionArea1 / rectAArea1) * 100f;
            float overlapPercentageC = (intersectionArea1 / rectCArea) * 100f;

            return overlapPercentageA >= percent * 100f;
        }


    }

}
