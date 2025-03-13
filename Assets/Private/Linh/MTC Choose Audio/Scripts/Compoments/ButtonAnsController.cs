using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Monkey.Game.MTCCA
{
    public class ButtonAnsController : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Button button;
        [SerializeField] private RectTransform rectItem;
        [SerializeField] private RectTransform rectImage;
        public AudioClip AudioClip { get; set; }
        public bool IsCorrect { get; set; }
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private int Index;
        [SerializeField] Image imageSpeak;
        private Vector3 originalPosition;
        private Vector3 imagePosition;
        private AnswerButton databutton;
        private bool isEnable;
        public AnswerButton Databutton { get => databutton; }
        public bool IsEnable { get => isEnable; set { isEnable = value; } }
        private int indexSlibing = 0;
        [SerializeField] private Image imageButton;
        private Color32 nomalColor;
        public CanvasGroup CanvasGroup { get => canvasGroup; }

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            originalPosition = button.transform.position;
            imagePosition = rectImage.transform.position;
       

        }
        private void OnClick()
        {
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, AudioClip, () => { });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            Debug.LogError("Click");
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Click, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
            transform.SetAsLastSibling();
        }
        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
        public void SetScale(Vector3 scale, float time)
        {
            transform.DOScale(scale, time).SetEase(Ease.Linear);
        }
        public void ChangeImageColor(string hexColor)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                imageSpeak.color = color;
            }
            else
            {
                Debug.LogWarning("Invalid hex color code.");
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.OnDrag, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
            indexSlibing = transform.GetSiblingIndex();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectItem.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
            rectItem.localPosition = localPoint;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
             if (CheckTriggerOfTwoObject(rectItem, rectImage, 0.2f))
             {
                AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.EndDrag, this);
                ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
       
             }
            else
                {
                ReturnButtonToOriginalPosition();

            }
        }
       
        public void OnActionWrong(System.Action callback = null)
        {
            StartCoroutine(PlayEffectMatchingWrongAnswer(gameObject, 1f, 0.25f, 10f, callback));
        }
        public static IEnumerator PlayEffectMatchingWrongAnswer(GameObject item, float scale = 1f, float duration = 0.25f, float? amplitude = 60f, Action? callback = null, bool isRunEffectLocal = true)
        {
            var defaultPos = item.transform.localPosition;
            var arg = new int[5] { 123, 250, 42, 193, 302 };
            var factor = 1f;
            for (int i = 0; i < 5; i++)
            {
                var px = (float)amplitude * factor * (float)Math.Cos(Mathf.Deg2Rad * arg[i]);
                var py = (float)amplitude * factor * (float)Math.Sin(Mathf.Deg2Rad * arg[i]);
                if (isRunEffectLocal)
                {
                    LeanTween.moveLocal(item, defaultPos + new Vector3(px, py, 0), duration / 6);
                }
                else
                {
                    LeanTween.move(item, defaultPos + new Vector3(px, py, 0), duration / 6);
                }
                factor -= 0.1f;
                yield return new WaitForSeconds(duration / 6);
            }
            LeanTween.moveLocal(item, defaultPos, duration / 6);
            yield return new WaitForSeconds(duration / 6);
            if (callback != null)
            {
                callback();
            }
        }

        public void ReturnButtonToOriginalPosition()
        {
            button.transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutQuad);
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
        public void SetColor()
        {
            if (IsCorrect)
                imageButton.color = Color.green;
            else
                imageButton.color = Color.red;
        }

        public void ResetColor()
        {
            imageButton.color = nomalColor;
        }
    }
}