using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class Game4FSMSystem : FSMSystem
    {
        private Game4InitState initState;
        private Game4IntroState introState;

        private void Awake()
        {
            initState = new Game4InitState();
            introState = new Game4IntroState();
        }

        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                Game4InitStateDependency initStateDependency = dependency.GetStateData<Game4InitStateDependency>();
                initState.SetUp(initStateDependency);

                Game4IntroStateDependency introStateDependency = dependency.GetStateData<Game4IntroStateDependency>();
                introState.SetUp(introStateDependency);
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
            }
        }
    }
}
