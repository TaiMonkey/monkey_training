using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Guiding : MonoBehaviour
    {
        [SerializeField] private Image handLong;
        [SerializeField] private Image handShort;
        [SerializeField] private CanvasGroup pointSpawnGuiding;
        [SerializeField] private CanvasGroup uiBlurGuiding;
        [SerializeField] private BaseButton buttonSkipGuiding;
        [SerializeField] private Transform pointCenter;
        private BESTW01Carousel carousel;
        private BESTW01GuidingConfig guidingConfig;
        private List<BESTW01CardItem> cardItems;
        private List<BESTW01Box> boxs;
        private bool isGuiding = false;
        private CanvasGroup canvasGroup;
        private Coroutine coroutineGuiding;

        private BESTW01CardItem tempItemDrag;
        private BESTW01CardItem cardGuiding;
        private BESTW01Box boxGuiding;
        private bool isWaitingCenter = false;


        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            SetColorImage(handLong, 1f);
            SetColorImage(handShort, 0f);
        }
        public void InitData(BESTW01GuidingConfig guidingConfig, List<BESTW01Box> boxs, BESTW01Carousel carousel)
        {
            this.guidingConfig = guidingConfig;
            this.carousel = carousel;
            this.boxs = boxs;
        }
        public void InitData(List<BESTW01CardItem> cardItems)
        {
            this.cardItems = cardItems;
        }
        public void StartGuiding(bool isDelay)
        {
            ResetGuiding();
            isGuiding = true;
            transform.localScale = Vector3.one;
            coroutineGuiding = StartCoroutine(DoSomethingDelay(isDelay));

        }
        private void Update()
        {
            if (!isWaitingCenter && cardGuiding != null)
            {
                float distance = Vector3.Distance(cardGuiding.transform.position, pointCenter.position);
                if (distance <= BESTW01HandleData.MAX_DISTANCE)
                {
                    isWaitingCenter = true;
                }
                else
                {
                    isWaitingCenter = false;
                }
            }
        }
        private IEnumerator DoSomethingDelay(bool isDelay)
        {
            SoundChannel soundData;
            if (isDelay) yield return new WaitForSeconds(guidingConfig.secondWaitStartGuiding);
            while (isGuiding)
            {
                if (tempItemDrag == null)
                {
                    if (BESTW01HandleData.CheckAlphaItem(cardItems))
                    {
                        cardGuiding = BESTW01HandleData.FindNearestTransform(cardItems, pointCenter.transform, true);
                    }else
                    {
                        cardGuiding = BESTW01HandleData.FindNearestTransform(cardItems, pointCenter.transform, false);
                    }
                    boxGuiding = boxs.Find((item) => item.GetData().typeBox == cardGuiding.DataCard.typeBox);
                    tempItemDrag = Instantiate(cardGuiding, pointSpawnGuiding.transform, false);
                    tempItemDrag.transform.localPosition = Vector3.zero;
                    tempItemDrag.Enable(false);
                    tempItemDrag.transform.SetAsFirstSibling();
                    tempItemDrag.GetComponent<CanvasGroup>().alpha = 0f;
                }
                yield return new WaitUntil(() => cardGuiding.GetComponent<CanvasGroup>().alpha != 0);
                yield return new WaitUntil(() => isWaitingCenter);
                carousel.EnableMovingCarousel(false);
                carousel.EnableMovingConveyor(false);
                transform.position = cardGuiding.transform.position;
                if (tempItemDrag != null) tempItemDrag.transform.position = cardGuiding.transform.position;
               
                buttonSkipGuiding.gameObject.SetActive(true);
                uiBlurGuiding.gameObject.SetActive(true);
                uiBlurGuiding.DOFade(0.75f, guidingConfig.secondDelay).SetEase(Ease.Linear).WaitForCompletion();
                cardGuiding = null;
                yield return new WaitForSeconds(guidingConfig.secondDelay);
                canvasGroup.DOFade(1f, guidingConfig.secondDelay).SetEase(Ease.Linear).onComplete += () => {
                    buttonSkipGuiding.Enable(true);
                };
                pointSpawnGuiding.DOFade(1f, guidingConfig.secondDelay).SetEase(Ease.Linear);
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND, guidingConfig.sfxAppear);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                yield return new WaitForSeconds(guidingConfig.secondDelay + (guidingConfig.secondDelay / 2));

                soundData = new SoundChannel(SoundChannel.PLAY_SOUND, guidingConfig.sfxClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                SetColorImage(handLong, 0f);
                SetColorImage(handShort, 1f);
                yield return new WaitForSeconds(guidingConfig.secondDelay / 2);
                if (tempItemDrag != null) {
                    tempItemDrag.GetComponent<CanvasGroup>().alpha = 0.6f;
                    tempItemDrag.transform.DOMove(boxGuiding.transform.position, 1f).SetEase(Ease.InOutQuad); 
                }
                yield return transform.DOMove(boxGuiding.transform.position, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
                yield return new WaitForSeconds(guidingConfig.secondDelay / 2);
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND, guidingConfig.sfxUnClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                SetColorImage(handLong, 1f);
                SetColorImage(handShort, 0f);
                yield return new WaitForSeconds(guidingConfig.secondDelay / 2);
                pointSpawnGuiding.DOFade(0, guidingConfig.secondDelay).SetEase(Ease.Linear).onComplete += () => {
                    DestroyTempItem();
                };
                uiBlurGuiding.DOFade(0f, guidingConfig.secondDelay).SetEase(Ease.Linear).onComplete += () => {
                    buttonSkipGuiding.Enable(false);
                };
                yield return canvasGroup.DOFade(0, guidingConfig.secondDelay).SetEase(Ease.Linear).WaitForCompletion();
                buttonSkipGuiding.gameObject.SetActive(false);
                uiBlurGuiding.gameObject.SetActive(false);
                carousel.EnableMovingCarousel(true);
                carousel.EnableMovingConveyor(true);
                isWaitingCenter = false;
                yield return new WaitForSeconds(guidingConfig.secondWaitStartGuiding);
            }
        }


        public void ResetGuiding()
        {
            SoundManager.Instance.StopFx();
            isGuiding = false;
            isWaitingCenter = false;
            cardGuiding = null;
            if (!carousel.IsCarouseling()) carousel.EnableMovingCarousel(true);
            if (!carousel.IsConveyoring()) carousel.EnableMovingConveyor(true);
            if (uiBlurGuiding.gameObject.activeSelf)
            {
                buttonSkipGuiding.Enable(false);
                uiBlurGuiding.DOFade(0f, 0.1f).onComplete += () => { 
                    uiBlurGuiding.gameObject.SetActive(false);
                    buttonSkipGuiding.gameObject.SetActive(false);
                };
            }
            pointSpawnGuiding.DOFade(0f, 0.1f).SetEase(Ease.Linear).onComplete += () => {
                DestroyTempItem();
            };
            if (coroutineGuiding != null) StopCoroutine(coroutineGuiding);
            transform.DOKill();
            canvasGroup.DOFade(0f, 0.1f);
            transform.localScale = Vector3.zero;
        }

        private void DestroyTempItem()
        {
            if (tempItemDrag != null)
            {
                Destroy(tempItemDrag);
                BESTW01HandleData.DestroyItem(pointSpawnGuiding.transform);
            }
        }


        private void SetColorImage(Image image, float indexColor)
        {
            Color currentColor = image.color;
            currentColor.a = indexColor;
            image.color = currentColor;
        }
    }
}