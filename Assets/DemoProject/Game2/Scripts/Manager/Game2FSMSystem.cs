using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    public class Game2FSMSystem : FSMSystem
    {
        private Game2InitState initState;
        private Game2IntroState introState;
        private Game2GuidingState guidingState;
        private Game2GamePlayState gamePlayState;
        private Game2DelayRightState delayRightState;

        private void Awake()
        {
            initState = new Game2InitState();
            introState = new Game2IntroState();
            guidingState = new Game2GuidingState();
            gamePlayState = new Game2GamePlayState();
            delayRightState = new Game2DelayRightState();
        }

        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                Game2InitStateDependency initStateDependency = dependency.GetStateData<Game2InitStateDependency>();
                initState.SetUp(initStateDependency);

                Game2IntroStateDependency introStateDependency = dependency.GetStateData<Game2IntroStateDependency>();
                introState.SetUp(introStateDependency);

                Game2GuidingStateDependency guidingStateDependency = dependency.GetStateData<Game2GuidingStateDependency>();
                guidingState.SetUp(guidingStateDependency);
            }
        }

        public override void GotoState(string eventName, object data)
        {
            StateName.Name state = (StateName.Name)Enum.Parse(typeof(StateName.Name), eventName);
            switch (state)
            {
                case StateName.Name.Init:
                    GotoState(initState, data);
                    break;
                case StateName.Name.Intro:
                    GotoState(introState, data);
                    break;
                case StateName.Name.Guiding:
                    GotoState(guidingState);
                    break;
                case StateName.Name.GamePlay:
                    GotoState(gamePlayState, data);
                    break;
                case StateName.Name.Delay:
                   GotoState(delayRightState, data);
                    break;
            }
        }
    }
}