using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;


namespace Monkey.Game.Game2Demo
{
    public class Game2Manager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            SetData("");
            fSMSystem.SetupStateData(dependency);
            this.ObserverStartListening<StateChanel>();

            Game2InitStateData initStateData = adapter.GetData<Game2InitStateData>(0);
            fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
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

        public void OnMMEvent(StateChanel eventType)
        {
            switch (eventType.StatusOfState)
            {
                case StateName.Status.InitEnd:
                    Game2IntroStateData introStateData = adapter.GetData<Game2IntroStateData>(0);
                    fSMSystem.GotoState(StateName.Name.Intro.ToString(), introStateData);
                    break;
                case StateName.Status.IntroEnd:
                case StateName.Status.PlayEnd:
                    fSMSystem.GotoState(StateName.Name.Guiding.ToString(), null);
                    break;
                case StateName.Status.OnClick:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), null);
                    break;
            }
        }
    }
}
