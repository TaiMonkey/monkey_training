using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    public class BreakTheEggFSMSytem : FSMSystem
    {
        private InitState initState;
        private IntroState introState;
        private GameplayState gameplayState;

        private void Awake()
        {
            initState = new InitState();
            introState = new IntroState();
            gameplayState = new GameplayState();
        }

        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                InitStateDependency initStateDependency = dependency.GetStateData<InitStateDependency>();
                initState.SetUp(initStateDependency);

                IntroStateDependency introStateDependency = dependency.GetStateData<IntroStateDependency>();
                introState.SetUp(introStateDependency);

                GameplayStateDependency gameplayStateDependency = dependency.GetStateData<GameplayStateDependency>();
                gameplayState.SetUp(gameplayStateDependency);
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
                    GotoState(introState);
                    break;
                case StateName.Name.GamePlay:
                    GotoState(gameplayState, data);
                    break;
            }
        }
    }
}