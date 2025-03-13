using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCManager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            //Start Fake setup data in adapter
            SetData("");
            // End Fake setup data
            this.ObserverStartListening<StateChanel>();

            base.Start();

            fSMSystem.SetupStateData(dependency);
            MTCInitStateData initStateData = adapter.GetData<MTCInitStateData>(StaticValue.CurrentTurn);
            fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<StateChanel>();
        }

        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }
        public void OnMMEvent(StateChanel eventType)
        {
            switch (eventType.StatusOfState)
            {
                case StateName.Status.InitFinish:
                    MTCIntroStateData introStateData = adapter.GetData<MTCIntroStateData>(0);
                    fSMSystem.GotoState(StateName.Name.Intro.ToString(), introStateData);
                    break;
                case StateName.Status.IntroFinish:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), null);
                    break;
                case StateName.Status.GuiddingStart:
                    fSMSystem.GotoState(StateName.Name.Guidding.ToString(), null);
                    break;
                case StateName.Status.GuiddingFinish:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), eventType.Data);
                    break;
                case StateName.Status.PlayFinish:
                    int maxTurn = adapter.GetMaxTurn();
                    fSMSystem.GotoState(StateName.Name.Delay.ToString(), maxTurn);
                    break;
                case StateName.Status.NexTurnStart:
                    MTCInitStateData initStateData = adapter.GetData<MTCInitStateData>(StaticValue.CurrentTurn);
                    fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
                    break;
                



            }
        }
    }
}
