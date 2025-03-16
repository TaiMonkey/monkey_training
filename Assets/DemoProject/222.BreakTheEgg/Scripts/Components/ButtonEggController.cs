using MonkeyBase.Observer;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Monkey.Game.BreakTheEgg
{
    public class ButtonEggController : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic spineEgg;
        [SerializeField] private TextMeshProUGUI alphabetAnswer;
        [SerializeField] private TextMeshProUGUI textAnswer;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private SkeletonGraphic spineFirework;
        [SerializeField] private Image imageFront;
        [SerializeField] private Material sourceMaterial;

        private UnityEngine.UI.Button button;
        private string originTextAnswer;
        public Vector3 OriginPos { get; set; }
        public Vector3 OriginalScale { get; private set; }
        public bool Isclicked { get; set; }
        public string TypeEgg { get; set; }
        public int Index { get; set; }

        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            textAnswer.transform.localScale = Vector3.zero;
            button.onClick.AddListener(OnClick);
            OriginalScale = transform.localPosition;
            SetActiveFirework(false);
        }

        public void ChangeColorOfCharacter(string text, char targetChar, string color)
        {
           string result = "";
           originTextAnswer = textAnswer.text;

           foreach (char c in text)
           {
               if (c == targetChar)
               {
                   result += $"<color={color}>{c}</color>";
               }
               else
               {
                   result += $"<color=black>{c}</color>";
               }
           }
            textAnswer.text = result;
        }

        public void ChangeTextColorToWhite()
        {
            textAnswer.text = originTextAnswer;
            textAnswer.color = Color.white;
        }

        public void SetLastSiblingImageFront()
        {
            imageFront.transform.SetAsLastSibling();
        }

        public TextMeshProUGUI GetTextAnswer()
        {
            return textAnswer;
        }

        public void SetAnimation(string skinName, bool loop)
        {
            spineEgg.AnimationState.SetAnimation(0, skinName, loop);
        }

        public void SetAnimationFirework(bool isLoop)
        {
            spineFirework.AnimationState.SetAnimation(0, "Phao hoa 1", isLoop);
        }

        public void SetActiveFirework(bool isActive)
        {
            spineFirework.gameObject.SetActive(isActive);
        }

        public TextMeshProUGUI GetAlphabetAnswer()
        {
            return alphabetAnswer;
        }

        public void SetAlphaBackgroundText(bool isHidden)
        {
            if (isHidden)
            {
                canvasGroup.alpha = 0;
            }
            else
            {
                canvasGroup.alpha = 1;
            }
        }

        public void SetAlphabet(string text)
        {
            alphabetAnswer.SetText(text);
        }

        public void SetTextAnswer(string text)
        {
            textAnswer.SetText(text);
        }

        public SkeletonGraphic GetSkeletonEgg()
        {
            return spineEgg;
        }

        public void SetScaleTextAnswer()
        {
            textAnswer.transform.localScale = Vector3.one;
        }

        private void OnClick()
        {
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Down, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }
    }
}
