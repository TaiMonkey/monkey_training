using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTrainingDemo
{
    public class ManagerBuoi3 : GameManager, EventListener<Buoi3Channer>
    {
        private int currentTurn;

        protected override void Awake()
        {
            base.Awake();
            string dataServeFake = "";
            SetData(dataServeFake);
        }

        protected override void Start()
        {
            MyMethod();
            currentTurn = 0;
        }
        void MyMethod()
        {
            fSMSystem.SetupStateData(dependency);
            DataDemoBuoi3 dataDemoBuoi3 = new DataDemoBuoi3();
            dataDemoBuoi3.CurrenTurn = adapter.GetData<DemoTurn>(currentTurn);
            dataDemoBuoi3.DataEvent = "";
            fSMSystem.GotoState(Buoi3State.InitData.ToString(), dataDemoBuoi3);
        }
        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.ObserverStartListening<Buoi3Channer>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            this.ObserverStopListening<Buoi3Channer>();
        }
        public void OnMMEvent(Buoi3Channer eventType)
        {
            LogMe.Log("Lucanhtai: " + eventType.State.ToString());
            switch (eventType.State)
            {
                case Buoi3StatusOfStateState.InitStateStart:
                    DataDemoBuoi3 dataDemoBuoi3 = new DataDemoBuoi3();
                    dataDemoBuoi3.CurrenTurn = adapter.GetData<DemoTurn>(currentTurn);
                    dataDemoBuoi3.DataEvent = (string)eventType.Data; ;
                    fSMSystem.GotoState(Buoi3State.InitData.ToString(), dataDemoBuoi3);
                    break;
                case Buoi3StatusOfStateState.InitStateEnd:
                case Buoi3StatusOfStateState.IntroStart:
                    string dataInitSend = (string)eventType.Data;
                    fSMSystem.GotoState(Buoi3State.Intro.ToString(), dataInitSend);
                    break;
                case Buoi3StatusOfStateState.IntroEnd:
                case Buoi3StatusOfStateState.GuidingStart:
                case Buoi3StatusOfStateState.DragResultEnd:
                case Buoi3StatusOfStateState.ClickEnd:
                    fSMSystem.GotoState(Buoi3State.Guiding.ToString(), null);
                    break;
                case Buoi3StatusOfStateState.DragginggStart:
                    DemoFishButton dataDragging = (DemoFishButton)eventType.Data;
                    fSMSystem.GotoState(Buoi3State.Draggingg.ToString(), dataDragging);
                    break;
                case Buoi3StatusOfStateState.DragResultStart:
                    DemoFishButton dataDrag = (DemoFishButton)eventType.Data;
                    fSMSystem.GotoState(Buoi3State.DragResult.ToString(), dataDrag);
                    break;
                case Buoi3StatusOfStateState.NextTurnStart:
                    if (currentTurn >= adapter.GetMaxTurn() - 1)
                    {
                        fSMSystem.GotoState(Buoi3State.Outro.ToString(), null);
                    }
                    else
                    {
                        ++currentTurn;
                        fSMSystem.GotoState(Buoi3State.NextTurn.ToString(), null);
                    }

                    break;
                default:
                    return;
            }

        }
    }
}
