using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public struct BESTW01CarouselChanner : EventListener<BESTW01CarouselChanner>
    {
        public BESTW01UserInput UserInput;
        public object Data;

        public BESTW01CarouselChanner(BESTW01UserInput userInput, object data)
        {
            this.UserInput = userInput;
            this.Data = data;
        }

        public void OnMMEvent(BESTW01CarouselChanner eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}