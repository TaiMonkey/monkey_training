
using MonkeyBase.Observer;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneNextTurnState : FSMState
    {
        private NextTurnStateDependency dependency;

        public override void SetUp(object data)
        {
            dependency = (NextTurnStateDependency)data;
        }

        public override void OnEnter()
        {
            Debug.LogError("GameOneNextTurnState");
            dependency.QuestionController.SetScale(Vector3.zero);
            dependency.ButtonSpeakerController.SetScale(Vector3.zero);
            List<ButtonAnswerController> listButtonAnswerControllers = dependency.ButtonAnswerControllers;
            for (int count = 0; count < listButtonAnswerControllers.Count; count++)
            {
                ButtonAnswerController buttonAnswerController = listButtonAnswerControllers[count];
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
        public QuestionLabelController QuestionController { get; set; }
        public ButtonSpeakerController ButtonSpeakerController { get; set; }
        public List<ButtonAnswerController> ButtonAnswerControllers { get; set; }
    }
}

