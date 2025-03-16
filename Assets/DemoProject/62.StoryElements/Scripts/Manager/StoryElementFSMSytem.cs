using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.StoryElement
{
    public class StoryElementFSMSytem : FSMSystem
    {
        private InitState initState;

        private void Awake()
        {
            initState = new InitState();
        }

        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                InitStateDependency initStateDependency = dependency.GetStateData<InitStateDependency>();
                initState.SetUp(initStateDependency);
            }
        }

        public override void GotoState(string eventName, object data)
        {
            StateName.Name state = (StateName.Name)Enum.Parse(typeof(StateName.Name), eventName);
            switch (state)
            {
                case StateName.Name.Init:
                    GotoState(initState);
                    break;
            }
        }
    }
}