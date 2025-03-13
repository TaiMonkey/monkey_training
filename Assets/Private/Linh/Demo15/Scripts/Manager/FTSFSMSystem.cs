using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.FTS
{
    public class FTSFSMSystem : FSMSystem
    {
        private FTSInitState initState;
        private void Awake()
        {
            initState = new FTSInitState();
        }
        public override void SetupStateData<T>(T data)
        {

            if (data is Dependency dependency)
            {
                FTSInitStateDependency initStateDependency = dependency.GetStateData<FTSInitStateDependency>();
                initState.SetUp(initStateDependency);
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
            }
        }
    }
}

