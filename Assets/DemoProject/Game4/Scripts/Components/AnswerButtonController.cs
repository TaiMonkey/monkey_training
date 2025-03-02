using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Monkey.Game.Game4Demo
{
    public class AnswerButtonController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] Image imageAnswer;
        [SerializeField] AudioClip SfxClick;
        [SerializeField] int index;
        private UnityEngine.UI.Button button;
        private SkeletonGraphic skeleton;
        private const string TAP = "Tap the dap an";

        private Vector3 startPosition;
        public Transform OriginalParent { get; set; }
        private bool isDragging = false;
        private bool isDropped = false;
        private bool isDraggable = false;
        private bool isEnable;

        [SerializeField] private RectTransform itemTransfrom;
        public AnswerSpawnerController spawnerController { get; set; }

        public bool IsBeingDragged => isDragging;


        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            skeleton = GetComponentInChildren<SkeletonGraphic>();
            button.onClick.AddListener(Onclick);
        }

        public void Initialize(AnswerSpawnerController spawner)
        {
            spawnerController = spawner;
        }

        public void SetSprite(Sprite image)
        {
            imageAnswer.sprite = image;
        }

        public void SetAudio(AudioClip audioClip)
        {
            this.SfxClick = audioClip;
        }


        public void Onclick()
        {
            skeleton.AnimationState.SetAnimation(0, TAP, false);
            Debug.LogError("Dang Click" + SfxClick.name);
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, SfxClick);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            spawnerController.PauseConveyor();
            spawnerController.IsAnyAnswerBeingDragged = true ;
            isDragging = true;
            isDropped = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(itemTransfrom.parent as RectTransform,
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
            itemTransfrom.localPosition = localPoint;
        }

        public void OnBackButton(Action callBack)
        {
            transform.DOMove(OriginalParent.position, 0.3f).SetEase(Ease.Linear).onComplete += () =>
            {
                callBack.Invoke();
            };
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            spawnerController.IsAnyAnswerBeingDragged = false;

            //bool isCorrect = CheckIfAnswerIsCorrect();
            bool isCorrect = false;

            if (isCorrect)
            {
                Debug.Log("✅ Đáp án đúng! Xóa khỏi danh sách hiển thị.");
                //spawnerController.RemoveCorrectAnswer(this);
            }
            else
            {
                Debug.LogError("❌ Đáp án sai! Đưa về hàng chờ.");
                spawnerController.RequeueAnswer(this);
            }

            spawnerController.ResumeConveyor();
        }
    }
}
