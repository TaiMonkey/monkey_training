using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTC
{
    public class ButtonAnsController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform itemTransform;
        [SerializeField] private Image khungImage;
        private Button button;
        private Color32 nomalColor;


        public AudioClip AudioClip { get; set; }
        public bool IsCorrect { get; set; }
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        public void SetImage(Sprite Image)
        {
            image.sprite = Image;
        }
        public void SetScale(Vector3 scale, float time)
        {
            transform.DOScale(scale, time).SetEase(Ease.Linear);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
        private void OnClick()
        {
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Answer, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }
        public void SetColor()
        {
            if (IsCorrect)
                khungImage.color = Color.green;
            else
                khungImage.color = Color.red;
        }

        public void ResetColor()
        {
            khungImage.color = nomalColor;
        }

    }
}
