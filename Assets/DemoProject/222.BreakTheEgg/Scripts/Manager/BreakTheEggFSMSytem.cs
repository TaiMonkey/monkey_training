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
        private ResultKnockEggState resultKnockEggState;
        private GuidingState guidingState;

        private void Awake()
        {
            initState = new InitState();
            introState = new IntroState();
            gameplayState = new GameplayState();
            resultKnockEggState = new ResultKnockEggState();
            guidingState = new GuidingState();
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

                ResultKnockEggStateDependency resultKnockEggStateDependency = dependency.GetStateData<ResultKnockEggStateDependency>();
                resultKnockEggState.SetUp(resultKnockEggStateDependency);

                GuidingStateDependency guidingStateDependency = dependency.GetStateData<GuidingStateDependency>();
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
                    GotoState(introState);
                    break;
                case StateName.Name.GamePlay:
                    GotoState(gameplayState, data);
                    break;
                case StateName.Name.ResultKnockEgg:
                    GotoState(resultKnockEggState, data);
                    break;
                case StateName.Name.Guiding:
                    GotoState(guidingState);
                    break;
            }
        }
    }
}