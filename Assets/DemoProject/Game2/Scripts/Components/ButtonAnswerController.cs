using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.Game2Demo
{
    public class ButtonAnswerController : MonoBehaviour
    {
        [SerializeField] private Image dataImage;
        public bool IsCorrect { get; set; }
        public AudioClip AudioClip { get; set; }

        private Image imageBackGround;
        private UnityEngine.UI.Button button;
        private Color32 nomalColor;
        private Vector3 initialPos;
        public SkeletonGraphic animState { get; set; }


        void Start()
        {
            imageBackGround = GetComponent<Image>();
            button = GetComponent<UnityEngine.UI.Button>();
            animState = GetComponentInChildren<SkeletonGraphic>();

            nomalColor = imageBackGround.color;
            initialPos = button.transform.localPosition;
            button.onClick.AddListener(OnClick);
        }

        public void SetSprite(Sprite sprite)
        {
            this.dataImage.sprite = sprite;
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public void SetColor()
        {
            if (IsCorrect)
                imageBackGround.color = Color.green;
            else
                imageBackGround.color = Color.red;
        }

        private void OnClick()
        {
            if(!IsCorrect)
            {
                StaticValue.CountWrong++;
            }
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Answer, this);
            ObserverManager.TriggerEvent(answerChanel);
        }

        public void ResetColor()
        {
            imageBackGround.color = nomalColor;
        }

        public async Task AnimWrong()
        {
            float duration = 0.25f;
            float baseAmplitude = Mathf.Min(button.GetComponent<RectTransform>().rect.width,
                                            button.GetComponent<RectTransform>().rect.height) * 0.18f;

            Sequence seq = DOTween.Sequence();

            seq.Append(button.transform.DOLocalMoveX(initialPos.x + baseAmplitude, duration / 5))
               .Append(button.transform.DOLocalMoveX(initialPos.x - baseAmplitude * 0.9f, duration / 5))
               .Append(button.transform.DOLocalMoveX(initialPos.x + baseAmplitude * 0.8f, duration / 5))
               .Append(button.transform.DOLocalMoveX(initialPos.x - baseAmplitude * 0.7f, duration / 5))
               .Append(button.transform.DOLocalMoveX(initialPos.x + baseAmplitude * 0.6f, duration / 5))
               .Append(button.transform.DOLocalMoveX(initialPos.x, duration / 5));

            seq.SetEase(Ease.Linear);
            await seq.AsyncWaitForCompletion();
        }

    }
}
