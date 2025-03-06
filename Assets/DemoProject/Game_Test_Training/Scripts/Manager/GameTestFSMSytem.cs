using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestFSMSytem : FSMSystem
    {
        private GameTestInitState initState;
        private GameTestGamePlayState gamePlayState;
        private GameTestGuidingState guidingState;
        private GameTestEndGameState endGameState;

        private void Awake()
        {
            initState = new GameTestInitState();
            gamePlayState = new GameTestGamePlayState();
            guidingState = new GameTestGuidingState();
            endGameState = new GameTestEndGameState();
        }
        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                GameTestInitStateDependency initStateDependency = dependency.GetStateData<GameTestInitStateDependency>();
                initState.SetUp(initStateDependency);

                GameTestGamePlayStateDependency gamePlayDependency = dependency.GetStateData<GameTestGamePlayStateDependency>();
                gamePlayState.SetUp(gamePlayDependency);

                GameTestGuidingStateDependency guidingDependency = dependency.GetStateData<GameTestGuidingStateDependency>();
                guidingState.SetUp(guidingDependency);

                GameTestEndGameStateDependency endGameDependency = dependency.GetStateData<GameTestEndGameStateDependency>();
                endGameState.SetUp(endGameDependency);
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
                case StateName.Name.GamePlay:
                    GotoState(gamePlayState, data);
                    break;
                case StateName.Name.Guiding:
                    GotoState(guidingState);
                    break;
                case StateName.Name.EndGame:
                    GotoState(endGameState);
                    break;
            }
        }
    }
}
