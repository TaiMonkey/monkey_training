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

        private void Awake()
        {
            initState = new GameTestInitState();
            gamePlayState = new GameTestGamePlayState();
        }
        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                GameTestInitStateDependency initStateDependency = dependency.GetStateData<GameTestInitStateDependency>();
                initState.SetUp(initStateDependency);

                GameTestGamePlayStateDependency gamePlayDependency = dependency.GetStateData<GameTestGamePlayStateDependency>();
                gamePlayState.SetUp(gamePlayDependency);
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
                    GotoState(gamePlayState);
                    break;
            }
        }
    }
}
