using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCInitState : FSMState
    {
        private MTCInitStateDependency dependency;
        private MTCInitStateData stateData;

        public override void SetUp(object data)
        {
            dependency = (MTCInitStateDependency)data;
        }
        public override void OnEnter(object data)
        {
           
            stateData = (MTCInitStateData)data;
            dependency.LabelQuestionController.SetText(stateData.DataQuestion);
            dependency.LabelQuestionController.SetScale(Vector3.zero);
            dependency.ButtonSpeakController.AudioClip = stateData.AudioQuestion;
            dependency.ButtonSpeakController.SetScale(Vector3.zero);
            List<ButtonAnsData> listButtonAnswerDatas = stateData.ListButtonsAnsData;
            for (int count = 0; count < listButtonAnswerDatas.Count; count++)
            {
                ButtonAnsData buttonAnswerData = listButtonAnswerDatas[count];

                ButtonAnsController buttonAnswerController = dependency.ButtonAnsControllers[count];

                buttonAnswerController.SetImage(buttonAnswerData.Data);
                buttonAnswerController.AudioClip = buttonAnswerData.audioClip;
                buttonAnswerController.IsCorrect = buttonAnswerData.IsCorrect;
                buttonAnswerController.SetScale(Vector3.zero);
            }
            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }


    }
    public class MTCInitStateDependency
    {
        public ButtonSpeakController ButtonSpeakController { get; set; }
        public LabelQuestionController LabelQuestionController { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
    }
    public class MTCInitStateData
    {
        public string DataQuestion { get; set; }
        public AudioClip AudioQuestion { get; set; }
        public List<ButtonAnsData> ListButtonsAnsData { get; set; }
    }

    public class ButtonAnsData
    {
        public Sprite Data { get; set; }
        public AudioClip audioClip { get; set; }
        public bool IsCorrect { get; set; }
    }
}
