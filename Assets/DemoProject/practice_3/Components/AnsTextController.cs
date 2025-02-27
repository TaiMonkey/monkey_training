using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Monkey.Game.Game3Demo
{
    public class AnsTextController : MonoBehaviour
    {
        private TextMeshProUGUI textAns;
        public int Index { get; set; }

        void Start()
        {
            textAns = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            this.textAns.text = text;
        }
    }
}
