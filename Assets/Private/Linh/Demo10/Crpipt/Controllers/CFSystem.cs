using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.CF
{

    public class CFSystem : FSMSystem
    {
        private CFInitState cFInit;
        private CFIntroState cfIntro;
        private CFPlayGameState cfPlay;
        private void Awake()
        {
            cFInit = new CFInitState();
            cfIntro = new CFIntroState();
            cfPlay = new CFPlayGameState();
        }
        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                CFInitStateObjectDependency initStateData = dependency.GetStateData<CFInitStateObjectDependency>();
                cFInit.SetUp(initStateData);
                CFIntroStateDependency introStateData = dependency.GetStateData<CFIntroStateDependency>();
                cfIntro.SetUp(introStateData);
            }
        }
        public override void GotoState(string eventName, object data)
        {
            StateName.Name state = (StateName.Name)Enum.Parse(typeof(StateName.Name), eventName);
            switch (state)
            {
                case StateName.Name.Init:
                    GotoState(cFInit, data);
                    break;
                case StateName.Name.Intro:
                    GotoState(cfIntro, data);
                    break;
                case StateName.Name.GamePlay:
                    GotoState(cfPlay, data);
                    break;

            }
        }
    }
}
