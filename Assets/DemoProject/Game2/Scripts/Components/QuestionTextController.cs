using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    public class QuestionTextController : MonoBehaviour
    {
        private TextMeshProUGUI labelQuestion;

        void Start()
        {
            labelQuestion = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            this.labelQuestion.text = text;
        }
    }
}
