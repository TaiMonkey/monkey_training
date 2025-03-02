using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class Game4Manager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            SetData("");
            this.ObserverStartListening<StateChanel>();

            base.Start();

            fSMSystem.SetupStateData(dependency);
            Game4InitStateData initStateData = adapter.GetData<Game4InitStateData>(0);
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
                    fSMSystem.GotoState(StateName.Name.Intro.ToString(), null);
                    break;
            }
        }
    }
}
