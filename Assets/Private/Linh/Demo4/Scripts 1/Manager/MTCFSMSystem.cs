using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCFSMSystem : FSMSystem
    {
        private MTCInitState initState;
        private MTCIntroState introState;
        private MTCPlayGameState playGameState;
        private MTCDelayFinishState delayFinishState;
        private MTCGuidingState guidingState;
        private MTCNextTurnState nextTurnState;

        private void Awake()
        {
            initState = new MTCInitState();
            introState = new MTCIntroState();
            playGameState = new MTCPlayGameState();
            delayFinishState = new MTCDelayFinishState();
            nextTurnState = new MTCNextTurnState();
            guidingState = new MTCGuidingState();

        }
        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                MTCInitStateDependency initStateDependency = dependency.GetStateData<MTCInitStateDependency>();
                initState.SetUp(initStateDependency);

                MTCIntroStateDependency introStateDependency = dependency.GetStateData<MTCIntroStateDependency>();
                introState.SetUp(introStateDependency);

                MTCGuidingDependency guidingDependency = dependency.GetStateData<MTCGuidingDependency>();
                guidingState.SetUp(guidingDependency);

                MTCDelayFinishStateDependency delayFinishStateDependency = dependency.GetStateData<MTCDelayFinishStateDependency>();
                delayFinishState.SetUp(delayFinishStateDependency);

                NextTurnStateDependency nextTurnStateDependency = dependency.GetStateData<NextTurnStateDependency>();
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
                    GotoState(playGameState, data);
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
