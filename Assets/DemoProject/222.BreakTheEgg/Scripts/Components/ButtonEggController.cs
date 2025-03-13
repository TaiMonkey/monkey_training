using MonkeyBase.Observer;
using Spine.Unity;
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

        private UnityEngine.UI.Button button;
        public Vector3 OriginPos { get; set; }
        public Vector3 OriginalScale { get; private set; }
        public bool Isclicked { get; set; }
        public string TypeEgg { get; set; }

        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            textAnswer.transform.localScale = Vector3.zero;
            button.onClick.AddListener(OnClick);
            OriginalScale = transform.localPosition;
            SetActiveFirework(false);
        }

        public void SetLastSiblingImageFront()
        {
            Debug.LogError($"SetLastSiblingImageFront {transform.parent.name}");
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

        public void SetAnimationFirework()
        {
            spineFirework.AnimationState.SetAnimation(0, "Phao hoa 1", false);
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
