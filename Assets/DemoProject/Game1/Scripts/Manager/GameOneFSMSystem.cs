using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneFSMSystem : FSMSystem
    {
        private GameOneInitState initState;
        private GameOneIntroState introState;
        private GameOneGamePlayState gamePlayState;
        private GameOneNextTurnState nextTurnState;
        private GameOneGuidingState guidingState;

        private void Awake()
        {
            initState = new GameOneInitState();
            introState = new GameOneIntroState();
            gamePlayState = new GameOneGamePlayState();
            nextTurnState = new GameOneNextTurnState();
            guidingState = new GameOneGuidingState();
        }

        public override void SetupStateData<T>(T data)
        {
            
            if (data is Dependency dependency)
            {
                GameOneInitStateDependency initStateDependency = dependency.GetStateData<GameOneInitStateDependency>();
                initState.SetUp(initStateDependency);

                GameOneIntroStateDependency introStateDependency = dependency.GetStateData<GameOneIntroStateDependency>();
                introState.SetUp(introStateDependency);

                NextTurnStateDependency nextTurnStateDependency = dependency.GetStateData<NextTurnStateDependency>();
                nextTurnState.SetUp(nextTurnStateDependency);

                GameOneGuidingStateDependency guidingStateDependency = dependency.GetStateData<GameOneGuidingStateDependency>();
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
                case StateName.Name.GamePlay:
                    GotoState(gamePlayState, data);
                    break;
                case StateName.Name.NextTurn:
                    GotoState(nextTurnState);
                    break;
                case StateName.Name.Guiding:
                    GotoState(guidingState);
                    break;
            }
        }       
    }
}
