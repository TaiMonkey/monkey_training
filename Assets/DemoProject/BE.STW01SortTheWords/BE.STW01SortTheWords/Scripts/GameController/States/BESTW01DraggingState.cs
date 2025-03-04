using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01DraggingState : FSMState
    {
        private BESTW01DraggingStateObjectDependency dependency;
        private CancellationTokenSource cts;
        private int timeDelay = 150;


        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            BESTW01DraggingStateEventData draggingStateDataEvent = (BESTW01DraggingStateEventData)data;
            DoWork(draggingStateDataEvent);
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01DraggingStateObjectDependency)data;
        }

        private void DoWork(BESTW01DraggingStateEventData eventData)
        {
            cts = new CancellationTokenSource();
            BESTW01CardItem objectDrag = eventData.ObjectEvent.GetComponent<BESTW01CardItem>();
            //BESTW01HandleData.EnableCards(dependency.CardItems, objectDrag.IdCard, false);
            dependency.BoxLeft.Enable(false);
            dependency.BoxRight.Enable(false);
            
        }


        public override void OnExit()
        {
            base.OnExit();
            cts?.Cancel();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Cancel();
            cts?.Dispose();
        }
    }
    public class BESTW01DraggingStateEventData
    {
        public GameObject ObjectEvent { get; set; }
    }

    public class BESTW01DraggingStateObjectDependency
    {
        public List<BESTW01CardItem> CardItems { get; set; }
        public BESTW01Box BoxLeft { get; set; }
        public BESTW01Box BoxRight { get; set; }
    }
}