
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class QuestionLabelController : MonoBehaviour
    {
        private TextMeshProUGUI labelQuestion;

        private void Awake()
        {
            labelQuestion = GetComponent<TextMeshProUGUI>();
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public void SetScale(Vector3 scale, float time)
        {
            transform.DOScale(scale, time).SetEase(Ease.OutBack);
        }

        public void SetText(string text)
        {
            this.labelQuestion.text = text;
        }
    }
}
