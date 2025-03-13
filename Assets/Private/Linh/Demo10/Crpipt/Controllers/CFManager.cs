using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.CF
{
    public class CFManager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            //Start Fake setup data in adapter
            SetData("");
            // End Fake setup data

            this.ObserverStartListening<StateChanel>();

            base.Start();

            fSMSystem.SetupStateData(dependency);



            CFInitStateData initStateData = adapter.GetData<CFInitStateData>(StaticValue.CurrentTurn);
            fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<StateChanel>();
        }

        public void OnMMEvent(StateChanel eventType)
        {
            switch (eventType.StatusOfState)
            {
                case StateName.Status.InitFinish:
                    CFIntroStateData introStateData = adapter.GetData<CFIntroStateData>(0);
                    fSMSystem.GotoState(StateName.Name.Intro.ToString(), introStateData);
                    break;
                case StateName.Status.IntroFinish:
                case StateName.Status.PlayStart:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), null);
                    break;
            }
        }
    }
}
