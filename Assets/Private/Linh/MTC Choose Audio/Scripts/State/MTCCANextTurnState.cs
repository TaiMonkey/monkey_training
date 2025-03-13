using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class MTCCANextTurnState : FSMState
    {
        private MTCCANextTurnStateDependency dependency;
        public override void SetUp(object data)
        {
            dependency = (MTCCANextTurnStateDependency)data;
        }
        public override void OnEnter(object data)
        {
            Debug.LogError("NextTurnState");
            base.OnEnter();
            dependency.PlaneAnimation.enabled = false;
            dependency.ButtonQuestionController.SetScale(Vector3.zero);
            List<ButtonAnsController> listButtonAnsControllers = dependency.ButtonAnsControllers;
            for (int count = 0; count < listButtonAnsControllers.Count; count++)
            {
                ButtonAnsController buttonAnsController = listButtonAnsControllers[count];
                buttonAnsController.SetScale(Vector3.zero);
                buttonAnsController.ResetColor();
              

            }
            StaticValue.CurrentTurn++;

            StateChanel stateChanel = new StateChanel(StateName.Status.NextTurnFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }
    public class MTCCANextTurnStateDependency
    {
        public SkeletonGraphic PlaneAnimation { get; set; }
        public ButtonQuestionController ButtonQuestionController { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
    }
}