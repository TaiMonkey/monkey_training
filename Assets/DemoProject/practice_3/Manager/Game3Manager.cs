using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game3Demo
{
    public class Game3Manager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            SetData("");
            fSMSystem.SetupStateData(dependency);
            //this.ObserverStartListening<StateChanel>();

            //Game3InitStateData initStateData = adapter.GetData<Game3InitStateData>(0);
            //fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
        }

        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //this.ObserverStopListening<StateChanel>();
        }

        public void OnMMEvent(StateChanel eventType)
        {
            switch (eventType.StatusOfState)
            {
                case StateName.Status.InitEnd:
                    break;
            }
        }
    }
}
