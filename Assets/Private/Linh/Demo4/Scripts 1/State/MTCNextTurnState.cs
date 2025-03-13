using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.MTC
{
    public class MTCNextTurnState : FSMState
    {
        private NextTurnStateDependency dependency;
        public override void SetUp(object data)
        {
            dependency = (NextTurnStateDependency)data;
        }

        public override void OnEnter()
        {
            Debug.LogError("GameOneNextTurnState");
            base.OnEnter();
            dependency.LabelQuestionController.SetScale(Vector3.zero);
            dependency.ButtonSpeakController.SetScale(Vector3.zero);
            List<ButtonAnsController> listButtonAnswerControllers = dependency.ButtonAnsControllers;
            for (int count = 0; count < listButtonAnswerControllers.Count; count++)
            {
                ButtonAnsController buttonAnswerController = listButtonAnswerControllers[count];
                buttonAnswerController.SetScale(Vector3.zero);
                buttonAnswerController.ResetColor();
            }
            StaticValue.CurrentTurn++;

            StateChanel stateChanel = new StateChanel(StateName.Status.NextTurnFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }

    public class NextTurnStateDependency
    {
        public ButtonSpeakController ButtonSpeakController { get; set; }
        public LabelQuestionController LabelQuestionController { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
    }
}
