using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonkeyBase.Observer;

namespace Monkey.Game.GameTest
{
    public class GameTestManager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            //Start Fake setup data in adapter
            //SetData("");
            // End Fake setup data

            this.ObserverStartListening<StateChanel>();

            base.Start();

            fSMSystem.SetupStateData(dependency);
            fSMSystem.GotoState(StateName.Name.Init.ToString(), null);
        }

        public void OnMMEvent(StateChanel eventType)
        {
            switch (eventType.StatusOfState)
            {
                case StateName.Status.InitFinish:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), null);
                    break;
            }
        }

        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<StateChanel>();
        }
    }
}
