using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.FTS
{
    public class FTSManager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            //Start Fake setup data in adapter
            SetData("");
            // End Fake setup data

            this.ObserverStartListening<StateChanel>();

            base.Start();

            fSMSystem.SetupStateData(dependency);

            FTSInitStateData initStateData = adapter.GetData<FTSInitStateData>(StaticValue.CurrentTurn);
            //Debug.LogError(initStateData.ListFishAnsDatas + " bbbbbbbbb");
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
            
        }
    }
}
