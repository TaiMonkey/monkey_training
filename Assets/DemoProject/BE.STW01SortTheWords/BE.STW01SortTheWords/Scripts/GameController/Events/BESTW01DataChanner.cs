using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public struct BESTW01DataChanner : EventListener<BESTW01DataChanner>
    {
        public BESTW01State EventName;
        public object Data;

        public BESTW01DataChanner(BESTW01State nameEvent, object data)
        {
            this.EventName = nameEvent;
            this.Data = data;
        }

        public void OnMMEvent(BESTW01DataChanner eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}