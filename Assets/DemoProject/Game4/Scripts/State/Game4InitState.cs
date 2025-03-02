using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class Game4InitState : FSMState
    {
        private Game4InitStateDependency dependency;
        private Game4InitStateData initStateData;

        public override void SetUp(object data)
        {
            dependency = (Game4InitStateDependency)data;
        }

        public override void OnEnter(object data)
        {
            initStateData = (Game4InitStateData)data;
            // Get data answer
            for(int i = 0; i < initStateData.ButtonAnswers.Count; i++)
            {
                ButtonAnswer buttonAnswer = initStateData.ButtonAnswers[i];
                AnswerButtonController answerButtonController = dependency.answerButtonControllers[i];

                answerButtonController.SetSprite(buttonAnswer.DataAnswer);
                answerButtonController.SetAudio( buttonAnswer.AudioClip);
            }   
            // Get data Box Question
            for (int i = 0; i < initStateData.ButtonBoxQues.Count; i++)
            {
                ButtonBoxQues buttonBoxQues = initStateData.ButtonBoxQues[i];
                BoxQuesController boxQuesController = dependency.boxQuesControllers[i];

                boxQuesController.SetTextQuestion(buttonBoxQues.DataQues);
                boxQuesController.SetAudio(buttonBoxQues.AudioClip);
            }
            // End state
            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }

    public class Game4InitStateDependency
    {
        public List<BoxQuesController> boxQuesControllers;
        public List<AnswerButtonController> answerButtonControllers;
    }

    public class Game4InitStateData
    {
        public List<ButtonBoxQues> ButtonBoxQues { get; set; }
        public List<ButtonAnswer> ButtonAnswers { get; set; }
        public AudioClip AudioClip { get; set; }
    }

    public class ButtonAnswer
    {
        public Sprite DataAnswer { get; set; }
        public AudioClip AudioClip { get; set; }
    }

    public class ButtonBoxQues
    {
        public string DataQues { get; set; }
        public AudioClip AudioClip { get; set; }
    }
}
