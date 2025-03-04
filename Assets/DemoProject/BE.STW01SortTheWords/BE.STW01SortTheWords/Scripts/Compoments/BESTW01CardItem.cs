using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01CardItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform itemTransfrom;
        [SerializeField] private Image image;
        [SerializeField] private Image outline;
        [SerializeField] private SkeletonGraphic skeletonCard;
        [SerializeField] private SkeletonUtilityBone boneFollow;
        private int idCard;
        private int indexPointCard;
        private List<BESTW01Box> boxs;
        private BESTW01CardConfig cardConfig;
        private BESTW01CardData data;
        private BESTW01Guiding guiding;
        private BESTW01Carousel carousel;
        private BESTW01Box currentBox;
        private BESTW01BoxConfig boxConfig;
        private BESTW01SDragResultConfig dragResultConfig;
        private bool isPlayAudio = false;
        private bool isEnable = false;
        private bool isDragged = false;
        private bool isDragging = false;
        private bool isDraggable = false;
        private float moveDuration = 0.5f;
        private Coroutine coroutineTap;

        public int IdCard  { get { return idCard; } }
        public BESTW01CardData DataCard { get { return data; } }
        public bool IsDragged
        {
            set { isDragged = value; }
            get { return isDragged; }
        }
       
        public int IndexPointCard
        {
            set { indexPointCard = value; }
            get { return indexPointCard; }
        }

        public void InitData(BESTW01CardData data, int idCard, int indexPointCard, List<BESTW01Box> boxs, BESTW01Carousel carousel,
            BESTW01Guiding guiding, BESTW01CardConfig cardConfig, BESTW01BoxConfig boxConfig, BESTW01SDragResultConfig dragResultConfig)
        {
            this.data = data;
            this.idCard = idCard;
            this.indexPointCard = indexPointCard;
            this.carousel = carousel;
            this.boxConfig = boxConfig;
            this.dragResultConfig = dragResultConfig;
            this.boxs = boxs;
            this.guiding = guiding;
            this.cardConfig = cardConfig;
            image.sprite = data.sprite;
            image.preserveAspect = true;
        }
                              

        public void Enable(bool value)
        {
            isEnable = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isEnable || isDraggable) return;
            transform.SetAsLastSibling();
            guiding.ResetGuiding();
            isDraggable = true;
            //isPlayAudio = false;
            BESTW01HandleData.SetAnimation(skeletonCard, cardConfig.tapCard, false, null);
            SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, cardConfig.sfxClick);
            ObserverManager.TriggerEvent<SoundChannel>(soundData);
            coroutineTap = StartCoroutine(OnTapItem());                 
          
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isDraggable) return;
            if (!isDragging)
            {
                BESTW01HandleData.SetAnimation(skeletonCard, cardConfig.unTapFirstCard, false, null);
                SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, cardConfig.sfxUnClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
            }
            else
            {
                isDragging = false;
                isEnable = false;
                bool foundMatch = false;
                BESTW01HandleData.SetAnimation(skeletonCard, cardConfig.unTapSecondCard, false, null);
                for (int i = 0; i < boxs.Count; i++)
                {
                    if (BESTW01HandleData.CheckTriggerOfTwoObject(outline.GetComponent<RectTransform>(), boxs[i].GetComponent<RectTransform>(), 0.08f))
                    {
                        foundMatch = true;
                        currentBox = boxs[i];
                        //BESTW01HandleData.TriggerStateInput(BESTW01UserInput.DragMatching, (gameObject, boxs[i].gameObject));
                        bool isCorrect = data.typeBox == currentBox.GetData().typeBox;
                        if (isCorrect)
                        {
                            StartCoroutine(OnCorrect());

                        }
                        else
                        {
                            StartCoroutine(OnWrong());
                        }
                        break;
                    }
                }
                if (!foundMatch)
                {

                    carousel.OnResetItemToCarousel(gameObject, () =>
                    {
                        BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
                    });
                    //BESTW01HandleData.TriggerStateCarousel(BESTW01UserInput.UnDragCardCarousel, (gameObject, true));
                }
            }
            isDraggable = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;
            if (!isDragging)
            {
                transform.DOKill();
                isDragging = true;
                BESTW01HandleData.TriggerStateCarousel(BESTW01UserInput.DragCardCarousel, gameObject);
                BESTW01HandleData.TriggerStateInput(BESTW01UserInput.Dragging, gameObject);
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(itemTransfrom.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
            itemTransfrom.localPosition = localPoint;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;
            transform.DOKill();
            isDragging = true;
           /* if (!isPlayAudio)
            {
                StopCoroutine(coroutineTap);
                isPlayAudio = true;
                SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, data.audio, () =>
                {
                    isPlayAudio = false;
                });
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
            }*/
            BESTW01HandleData.TriggerStateCarousel(BESTW01UserInput.DragCardCarousel, gameObject);
            BESTW01HandleData.TriggerStateInput(BESTW01UserInput.Dragging, gameObject);
        }

        private IEnumerator OnTapItem()
        {
            /*if (!isPlayAudio)
            {
                yield return new WaitForSeconds(0.5f);
                isPlayAudio = true;
                SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, data.audio, () =>
                {
                    isPlayAudio = false;
                });
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
            }*/
            yield return new WaitForSeconds(0.5f);
            SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, data.audio);
            ObserverManager.TriggerEvent<SoundChannel>(soundData);
        }

        private IEnumerator OnCorrect()
        {
            SendEventTracking(data.text, true);
            int turnBox = (currentBox.GetData().typeBox == BESTW01TypeBox.Left) ? BESTW01HandleData.CurrentTurnBoxLeft : BESTW01HandleData.CurrentTurnBoxRight;
            BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxNormal, (int)currentBox.GetData().typeBox, turnBox, false, null);
            _ = (currentBox.GetData().typeBox == BESTW01TypeBox.Left) ? BESTW01HandleData.CurrentTurnBoxLeft++ : BESTW01HandleData.CurrentTurnBoxRight++;
            int numberBox = (int)currentBox.GetData().typeBox;
            bool tscMoveDone = false;
            bool tscAnimationDone = false;
            transform.DOKill();
            isDragged = true;
            currentBox.IsPlaying = true;
            
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3 (currentBox.transform.position.x, currentBox.transform.position.y - 0.15f, currentBox.transform.position.z);

            Vector3 midPosition = (startPosition + endPosition) / 2 + Vector3.up * 4f;
            float highestY = float.MinValue;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOPath(new[] { startPosition, midPosition, endPosition }, moveDuration, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)).OnUpdate(() =>
                {
                    float currentY = transform.position.y;
                    if (currentY > highestY)
                    {
                        highestY = currentY; // Cập nhật giá trị cao nhất
                    }
                    else if (highestY > float.MinValue) // Phát hiện đã qua đỉnh
                    {
                        transform.SetParent(currentBox.transform);
                        transform.SetAsFirstSibling(); // Gọi callback chỉ một lần
                    }

                });
            boneFollow.scale = false;
            // Hiệu ứng scale: 0.8 -> 1 -> 0.7
            sequence.Join(boneFollow.transform.DOScale(0.95f, moveDuration * 0.2f).SetEase(Ease.InSine)) // Scale 0.8 -> 1
                .Append(boneFollow.transform.DOScale(0.7f, moveDuration * 0.35f).SetEase(Ease.OutSine)); // Scale 1 -> 0.7
           
            sequence.OnComplete(() =>
            {
                tscMoveDone = true;
            });
            yield return new WaitUntil(() => tscMoveDone);
            tscAnimationDone = false;

            transform.localScale = Vector3.zero;
          /*  BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxPopup, numberBox, turnBox, false, (trackEntry) => {
                tscAnimationDone = true;
            });
            yield return new WaitUntil(() => tscAnimationDone);
            tscAnimationDone = false;*/

            SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dragResultConfig.sfxCorrect);
            ObserverManager.TriggerEvent<SoundChannel>(soundData);
           
            BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxCorrect, numberBox, turnBox, false, (trackEntry) => {
                if ((turnBox + 1) == BESTW01HandleData.MAX_TURN_BOX)
                {
                    BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxGreen, numberBox, false, null);

                } else
                {
                    BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxNormal, numberBox, turnBox + 1, false, null);
                }
                tscAnimationDone = true;
                currentBox.IsPlaying = false;
            });
         
            bool tscFireDone = false;
            int randomIndex = UnityEngine.Random.RandomRange(0, boxConfig.fireworks.Length);
            SkeletonGraphic boxFirework = currentBox.GetFirework();
            boxFirework.gameObject.SetActive(true);

            BESTW01HandleData.SetAnimation(boxFirework, boxConfig.fireworks[randomIndex], false, (trackEntry) => {
                boxFirework.gameObject.SetActive(false);
                tscFireDone = true;
            });
            yield return new WaitUntil(() => tscAnimationDone && tscFireDone);
            BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
        }


        private IEnumerator OnWrong()
        {
            SendEventTracking(data.text, false);
            int turnBox = (currentBox.GetData().typeBox == BESTW01TypeBox.Left) ? BESTW01HandleData.CurrentTurnBoxLeft : BESTW01HandleData.CurrentTurnBoxRight;
            bool tscAnimationDone = false;
            SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dragResultConfig.sfxWrong);
            ObserverManager.TriggerEvent<SoundChannel>(soundData);
            carousel.OnResetItemToCarousel(gameObject, () => {});

            BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxWrong,
           (int)currentBox.GetData().typeBox, turnBox, false, (trackEntry) => {
               BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), boxConfig.boxNormal, (int)currentBox.GetData().typeBox, turnBox, false, null);
               tscAnimationDone = true;
           });
            yield return new WaitUntil(() => tscAnimationDone);
            BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
        }

        private void SendEventTracking(string tagert, bool isCorrect)
        {
            UserChooseAnswerData dataEventWrong = new UserChooseAnswerData();
           
            EventUserPlayGameChanel userEventWrong = new EventUserPlayGameChanel(EventUserPlayGameChanel.UserEvent.OtherReport, dataEventWrong);
            ObserverManager.TriggerEvent(userEventWrong);
        }


    }
}