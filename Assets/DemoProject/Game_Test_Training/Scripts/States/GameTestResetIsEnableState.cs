using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestResetIsEnableState : FSMState
    {
        private GameTestResetIsEnableStateDependency dependency;


        public override void SetUp(object data)
        {
            dependency = (GameTestResetIsEnableStateDependency)data;
        }

        public override void OnEnter()
        {
            Debug.LogError("GameTestResetIsEnableState");
            foreach(var item in dependency.BearButtonControllers)
            {
                if(!item.IsCorrect)
                {
                    item.IsEnable = true;
                }
            }
            foreach (var item in dependency.TigerButtonControllers)
            {
                if (!item.IsCorrect)
                {
                    item.IsEnable = true;
                }
            }
            StateChanel state = new StateChanel(StateName.Status.RestIsEnableFinish);
            ObserverManager.TriggerEvent(state);
        }
    }

    public class GameTestResetIsEnableStateDependency
    {
        public List<AnimalButtonController> TigerButtonControllers { get; set; }
        public List<AnimalButtonController> BearButtonControllers { get; set; }

    }
}