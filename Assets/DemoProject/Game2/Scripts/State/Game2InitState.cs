using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    public class Game2InitState : FSMState
    {
        private Game2InitStateDependency dependency;
        private Game2InitStateData initStateData;

        public override void SetUp(object data)
        {
            dependency = (Game2InitStateDependency)data;
        }

        public override void OnEnter(object Data)
        {
            initStateData = (Game2InitStateData)Data;
            dependency.TextQuestion.SetText(initStateData.DataQuestion);
            dependency.ButtonSpeaker.AudioClip = initStateData.AudioClipQuestion;

            for(int i = 0; i < dependency.ButtonAnswers.Count; i++)
            {
                ButtonAnswerController buttonAnswerController = dependency.ButtonAnswers[i];
                ButtonAnswerData buttonAnswerData = initStateData.buttonAnswerDatas[i];

                buttonAnswerController.IsCorrect = buttonAnswerData.IsCorrect;
                buttonAnswerController.SetSprite(buttonAnswerData.Data);
                buttonAnswerController.AudioClip = buttonAnswerData.audioClip;
                buttonAnswerController.SetScale(Vector3.zero);
            }

            // event
            StateChanel stateChanel = new StateChanel(StateName.Status.InitEnd);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }

    public class Game2InitStateDependency
    {
        public ButtonSpeakerController ButtonSpeaker { get; set; }
        public QuestionTextController TextQuestion { get; set; }
        public List<ButtonAnswerController> ButtonAnswers { get; set; }
    }

    public class Game2InitStateData
    {
        public string DataQuestion { get; set; }
        public AudioClip AudioClipQuestion { get; set; }
        public List<ButtonAnswerData> buttonAnswerDatas { get; set; }
    }

    public class ButtonAnswerData
    {
        public Sprite Data { get; set; }
        public AudioClip audioClip { get; set; }
        public bool IsCorrect { get; set; }
    }
}
