using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Monkey.Game.MTC
{
    public class LabelQuestionController : MonoBehaviour
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
            transform.DOScale(scale, time).SetEase(Ease.Linear);
        }

        public void SetText(string text)
        {
            this.labelQuestion.text = text;
        }
    }
}
