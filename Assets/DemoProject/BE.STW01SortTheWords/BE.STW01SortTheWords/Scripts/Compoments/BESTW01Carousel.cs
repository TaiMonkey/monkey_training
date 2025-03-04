using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    [Serializable]
    public class BESTW01Point
    {
        public int index;
        public Transform transform;
    }

    public class BESTW01Carousel : MonoBehaviour/*, EventListener<BESTW01CarouselChanner>*/
    {
        [SerializeField] private Transform pointStartCarousel;
        [SerializeField] private Transform pointEndCarousel;
        [SerializeField] private Transform pointStartConveyor;
        [SerializeField] private Transform pointEndConveyor;
        [SerializeField] private HorizontalLayoutGroup layoutGroupPointCard;
        [SerializeField] private HorizontalLayoutGroup layoutGroupConveyor;
        private HorizontalLayoutGroup layoutGroupCard;
        private List<BESTW01CardItem> cardItems;
        private List<Transform> pointCards;
        private List<Transform> conveyorItems;
        private List<BESTW01Point> pointCardItems;
        private float carouselSpeed = 1.5f;
        private float timeToReach = 0.35f;
        private bool isCarouseling = false;
        private bool isConveyoring = false;

        private void Start()
        {
            layoutGroupCard = GetComponent<HorizontalLayoutGroup>();
        }

        public void InitData(List<BESTW01CardItem> cardItems, List<Transform> pointCardItem, List<Transform> conveyorItems)
        {
            this.cardItems = cardItems;
            this.pointCards = pointCardItem;
            this.conveyorItems = conveyorItems;
            pointCardItems = new List<BESTW01Point>();
            for (int i = 0; i < pointCardItem.Count; i++)
            {
                int indexPoint = i % 6;
                BESTW01Point point = new BESTW01Point();
                point.index = indexPoint;
                point.transform = pointCardItem[i];
                pointCardItems.Add(point);
            }

            StartCoroutine(InitializeCards());
            StartCoroutine(InitializeConveyor());
        }

        public IEnumerator InitializeCards()
        {
            yield return new WaitForEndOfFrame();

            int countCard = cardItems.Count;
            Vector3[] arrPosCard = new Vector3[countCard];

            for (int i = 0; i < countCard; i++)
            {
                Vector3 vector = cardItems[i].GetComponent<RectTransform>().anchoredPosition;
                arrPosCard[i] = vector;
            }


            layoutGroupCard.enabled = false;
            layoutGroupPointCard.enabled = false;

            for (int i = 0; i < countCard; i++)
            {
                RectTransform cardRect = cardItems[i].GetComponent<RectTransform>();
                cardRect.anchoredPosition = arrPosCard[i];
            }

            for (int i = 0; i < pointCardItems.Count; i++)
            {
                RectTransform pointCardRect = pointCardItems[i].transform.GetComponent<RectTransform>();
                pointCardRect.anchoredPosition = arrPosCard[i];

                Transform transPointCard = pointCardItems[i].transform;
                transPointCard.position = new Vector3(transPointCard.position.x - carouselSpeed * timeToReach * 2, transPointCard.position.y, transPointCard.position.z);
            }

            Dictionary<int, RectTransform> firstPositionByIndexPoint = new Dictionary<int, RectTransform>();
            for (int i = 0; i < cardItems.Count; i++)
            {
                int indexPoint = i % 6;
                RectTransform rectTransform = cardItems[i].GetComponent<RectTransform>();

                if (!firstPositionByIndexPoint.ContainsKey(indexPoint))
                {
                    firstPositionByIndexPoint[indexPoint] = rectTransform;
                }
                else
                {
                    RectTransform firstRect = firstPositionByIndexPoint[indexPoint];
                    rectTransform.position = firstRect.position;
                    rectTransform.anchoredPosition = firstRect.anchoredPosition;
                    rectTransform.GetComponent<CanvasGroup>().alpha = 0f;
                    rectTransform.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    rectTransform.GetComponent<CanvasGroup>().interactable = false;
                }
            }
        }
        public IEnumerator InitializeConveyor()
        {
            yield return new WaitForEndOfFrame();

            int countConveyor = conveyorItems.Count;
            Vector3[] arrPosConveyor = new Vector3[countConveyor];

            for (int i = 0; i < countConveyor; i++)
            {
                Vector3 vector = conveyorItems[i].GetComponent<RectTransform>().anchoredPosition;
                arrPosConveyor[i] = vector;
            }
            layoutGroupConveyor.enabled = false;

            for (int i = 0; i < countConveyor; i++)
            {
                RectTransform conveyorRect = conveyorItems[i].GetComponent<RectTransform>();
                conveyorRect.anchoredPosition = arrPosConveyor[i];
            }
        }

        public bool IsCarouseling()
        {
            return this.isCarouseling;
        }
        public bool IsConveyoring()
        {
            return this.isConveyoring;
        }

        public void EnableMovingCarousel(bool value)
        {
            this.isCarouseling = value;
            if (this.isCarouseling)
            {
                for (int i = 0; i < cardItems.Count; i++)
                {
                    MoveCard(cardItems[i]);
                }
                for (int i = 0; i < pointCardItems.Count; i++)
                {
                    MoveCard(pointCardItems[i]);
                }
            }
            else
            {
                BESTW01HandleData.KillTweening(cardItems);
                BESTW01HandleData.KillTweening(pointCards);
            }
        }
        public void EnableMovingConveyor(bool value)
        {
            this.isConveyoring = value;
            if (this.isConveyoring)
            {
                for (int i = 0; i < conveyorItems.Count; i++)
                {
                    MoveConveyor(conveyorItems[i]);
                }
            }
            else
            {
                BESTW01HandleData.KillTweening(conveyorItems);
            }
        }
        private void MoveCard(BESTW01CardItem card)
        {
            if (card.IsDragged) return;
            card.transform.DOMoveX(pointEndCarousel.position.x, carouselSpeed)
                .SetEase(Ease.Linear).SetSpeedBased()
                .OnComplete(() => {
                    card.transform.position = new Vector3(pointStartCarousel.position.x, card.transform.position.y, card.transform.position.z);
                    if(card.GetComponent<CanvasGroup>().alpha == 0)
                    {
                        BESTW01CardItem cardAbove = cardItems.Find((item) => item.IndexPointCard == card.IndexPointCard && item != card);
                        if (cardAbove.IsDragged) {

                            card.GetComponent<CanvasGroup>().alpha = 1f;
                            card.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            card.GetComponent<CanvasGroup>().interactable = true;
                        }
                    }
                    MoveCard(card);
                });
        }

        private void MoveCard(BESTW01Point pointCard)
        {
            pointCard.transform.DOMoveX(pointEndCarousel.position.x, carouselSpeed)
                .SetEase(Ease.Linear).SetSpeedBased()
                .OnComplete(() => {
                    pointCard.transform.position = new Vector3(pointStartCarousel.position.x, pointCard.transform.position.y, pointCard.transform.position.z);
                    MoveCard(pointCard);
                });
        }
        private void MoveConveyor(Transform pointConveyor)
        {
            pointConveyor.DOMoveX(pointEndConveyor.position.x, carouselSpeed)
                .SetEase(Ease.Linear).SetSpeedBased()
                .OnComplete(() => {
                    pointConveyor.transform.position = new Vector3(pointStartCarousel.position.x, pointConveyor.position.y, pointConveyor.position.z);
                    MoveConveyor(pointConveyor);
                });
        }

        public void OnResetItemToCarousel(GameObject card, Action callBack)
        {
            BESTW01CardItem cardItemUnDrag = card.GetComponent<BESTW01CardItem>();
            Transform pointResetCard = pointCardItems.Find((item) => item.index == cardItemUnDrag.IndexPointCard).transform;
            float projectedXPosition = pointResetCard.position.x + carouselSpeed * timeToReach;
            Vector3 projectedPosition = new Vector3(projectedXPosition, pointResetCard.position.y, pointResetCard.position.z);
            cardItemUnDrag.transform.DOMove(projectedPosition, timeToReach)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => {
                cardItemUnDrag.DOKill();
                MoveCard(cardItemUnDrag);
                callBack?.Invoke();
            });
        }     


       /* public void OnMMEvent(BESTW01CarouselChanner eventType)
        {
            switch (eventType.UserInput)
            {
                case BESTW01UserInput.DragCardCarousel:
                    GameObject objectEventDrag = (GameObject)eventType.Data;

                    break;
                case BESTW01UserInput.UnDragCardCarousel:
                    (GameObject card, bool isTriggerState) dataEventUnDrag = ((GameObject card, bool isTriggerState))eventType.Data;
                    BESTW01CardItem cardItemUnDrag = dataEventUnDrag.card.GetComponent<BESTW01CardItem>();
                    cardItemUnDrag.SetPlayAudio(false);
                    Transform pointResetCard = pointCardItems.Find((item) => item.index == cardItemUnDrag.IndexPointCard).transform;
                    float projectedXPosition = pointResetCard.position.x + carouselSpeed * timeToReach;
                    Vector3 projectedPosition = new Vector3(projectedXPosition, pointResetCard.position.y, pointResetCard.position.z);
                    cardItemUnDrag.transform.DOMove(projectedPosition, timeToReach)
                    .SetEase(Ease.InOutCubic)
                    .OnComplete(() => {
                        cardItemUnDrag.DOKill();
                        MoveCard(cardItemUnDrag);
                        if(dataEventUnDrag.isTriggerState) BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
                    });

                    break;
            }
        }
    
        private void OnEnable()
        {
            this.ObserverStartListening<BESTW01CarouselChanner>();
        }

        private void OnDisable()
        {
            this.ObserverStopListening<BESTW01CarouselChanner>();
        }*/
    }
}