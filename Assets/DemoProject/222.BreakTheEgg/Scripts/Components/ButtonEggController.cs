using MonkeyBase.Observer;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Monkey.Game.BreakTheEgg
{
    public class ButtonEggController : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic spineEgg;
        [SerializeField] private TextMeshProUGUI alphabetAnswer;
        [SerializeField] private TextMeshProUGUI textAnswer;
        private UnityEngine.UI.Button button;
        public RectTransform OriginPos { get; set; }
        public bool Isclicked { get; set; }

        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            textAnswer.transform.localScale = Vector3.zero;
            OriginPos = GetComponent<RectTransform>();
            button.onClick.AddListener(OnClick);
        }

        public void SetAnimation(string skinName, bool loop)
        {
            spineEgg.AnimationState.SetAnimation(0, skinName, loop);
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
        private void OnClick()
        {
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Down, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }
    }
}
