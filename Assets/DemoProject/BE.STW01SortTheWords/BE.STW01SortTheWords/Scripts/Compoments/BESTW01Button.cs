using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Button : BaseButton
    {
        [SerializeField] BESTW01UserInput userInput;
        public override void Enable(bool isEnable)
        {
            button.interactable = isEnable;
        }

        public override void OnClick()
        {
            BESTW01HandleData.TriggerStateInput(userInput, gameObject);
        }
    }
}