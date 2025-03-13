using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class MTCCASystem : FSMSystem
    {
        private MTCCAInitState initState;
        private MTCCAIntroState introState;
        private MTCCAPlayState playState;
        private MTCCADelayFinishState delayFinishState;
        private MTCCANextTurnState nextTurnState;
        private MTCCAGuidingState guidingState;
        private void Awake()
        {
            initState = new MTCCAInitState();
            introState = new MTCCAIntroState();
            playState = new MTCCAPlayState();
            guidingState = new MTCCAGuidingState();
            delayFinishState = new MTCCADelayFinishState();
            nextTurnState = new MTCCANextTurnState();
            
        }

        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                MTCCAInitStateDependency initStateDependency = dependency.GetStateData<MTCCAInitStateDependency>();
                initState.SetUp(initStateDependency);

                MTCCAIntroStateDependency introStateDependency = dependency.GetStateData<MTCCAIntroStateDependency>();
                introState.SetUp(introStateDependency);

                MTCCAPlayStateDependency playStateDependency = dependency.GetStateData<MTCCAPlayStateDependency>();
                playState.SetUp(playStateDependency);

                MTCCAGuidingStateDependency guidingStateDependency = dependency.GetStateData<MTCCAGuidingStateDependency>();
                guidingState.SetUp(guidingStateDependency);

                MTCCADelayFinishStateDependency delayFinishStateDependency = dependency.GetStateData<MTCCADelayFinishStateDependency>();
                delayFinishState.SetUp(delayFinishStateDependency);

                MTCCANextTurnStateDependency nextTurnStateDependency = dependency.GetStateData<MTCCANextTurnStateDependency>();
                nextTurnState.SetUp(nextTurnStateDependency);

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
                case StateName.Name.GamePlay:
                    GotoState(playState, data);
                    break;
                case StateName.Name.Guidding:
                    GotoState(guidingState, data);
                    break;
                case StateName.Name.Delay:
                    GotoState(delayFinishState, data);
                    break;
                case StateName.Name.NextTurn:
                    GotoState(nextTurnState, data);
                    break;
            }
        }
    }
}
