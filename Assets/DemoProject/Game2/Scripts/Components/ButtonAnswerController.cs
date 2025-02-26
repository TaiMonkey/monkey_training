using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
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


        void Start()
        {
            imageBackGround = GetComponent<Image>();
            button = GetComponent<UnityEngine.UI.Button>();
            nomalColor = imageBackGround.color;
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
            StateChanel stateChanel = new StateChanel(StateName.Status.OnClick);
            ObserverManager.TriggerEvent(stateChanel);

            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Answer, this);
            ObserverManager.TriggerEvent(answerChanel);
        }

        public void ResetColor()
        {
            imageBackGround.color = nomalColor;
        }

    }
}
