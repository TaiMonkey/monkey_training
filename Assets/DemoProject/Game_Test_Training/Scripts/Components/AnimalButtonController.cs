using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Monkey.Game.GameTest
{
    public class AnimalButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
    {
        [SerializeField] SkeletonGraphic skeletonGraphic;
        [SerializeField] SkeletonGraphic AnimStart;
        [SerializeField] private RectTransform itemTransfrom;
        [SerializeField] private CanvasGroup canvasGroup;

        public RectTransform cageTranform { get; private set; }
        private Vector3 originalScale;
        public Vector3 OriginPos { get; set; }
        public Canvas ParentCanvas { get; set; }
        public Transform ParentSnapPoint { get; set; }
        public Transform OriginParent { get; set; }

        private bool isEnable = true;
        private bool isDragging = false;
        private bool isDraggable = false;
        private const string KEO_LOOP = "1.1 - Keo - Loop";
        public int Cage_Type { get; set; }
        public bool IsCorrect {get; set;} = false;
        public CanvasGroup CanvasGroup { get => canvasGroup; }



        public bool IsEnable { get => isEnable; set { isEnable = value; } }

        public void InitDataCage(RectTransform initDataCage)
        {
            this.cageTranform = initDataCage;
        }

        public void SetAnimStart(string anim)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, anim, true);
        }

        public SkeletonGraphic GetSkeletonGraphic()
        {
            return skeletonGraphic;
        }

        public SkeletonGraphic GetAnimStar()
        {
            return AnimStart;
        }

        public void SetScaleSkeleton(float scale, bool scaleUp)
        {
            if (scaleUp)
            {
                skeletonGraphic.transform.localScale *= scale;
            }
            else
            {
                skeletonGraphic.transform.localScale = originalScale;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isEnable || isDraggable) return;
            isDraggable = true;
            originalScale = skeletonGraphic.transform.localScale;

            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Down, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isEnable || !isDraggable) return;
            if (CheckTriggerOfTwoObject(itemTransfrom, cageTranform, 0.2f))
            {
                transform.SetParent(ParentSnapPoint.transform);
                IsCorrect = true;
                isEnable = false;

                AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Up, this);
                ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
            }
            else
            {
                StaticValue.CountWrong++;
                transform.SetParent(OriginParent.transform);
                Debug.Log("Fail");
                OnBackButton(() =>
                {
                    isEnable = true;
                });
                AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Up, this);
                ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
            }

            isDraggable = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isEnable || !isDraggable) return;
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.OnDrag, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(itemTransfrom.parent as RectTransform,
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
            itemTransfrom.localPosition = localPoint;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isEnable || !isDraggable) return;
            isDragging = true;

            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.BeginDrag, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
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

        public void OnBackButton(Action callBack)
        {
            transform.DOMove(OriginPos, 0.2f).SetEase(Ease.Linear).onComplete += () =>
            {
                callBack.Invoke();
            };
        }
    }
}