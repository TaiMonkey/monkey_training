
using System.Collections.Generic;
using UnityEngine;
using MonkeyBase.Observer;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneInitState : FSMState
    {
        private GameOneInitStateDependency dependency;
        private GameOneInitStateData stateData;
        public override void SetUp(object data)
        {
            dependency = (GameOneInitStateDependency)data;
        }

        public override void OnEnter(object data)
        {
            stateData = (GameOneInitStateData)data;

            dependency.QuestionController.SetText(stateData.DataQuestion);
            dependency.QuestionController.SetScale(Vector3.zero);
            dependency.ButtonSpeakerController.AudioClip = stateData.AudioClipQuesion;

            List<ButtonAnswerData> listButtonAnswerDatas = stateData.ListButtonAnswerDatas;
            for(int count = 0; count < listButtonAnswerDatas.Count; count++ )
            {
                ButtonAnswerData buttonAnswerData = listButtonAnswerDatas[count];

                ButtonAnswerController buttonAnswerController = dependency.ButtonAnswerControllers[count];

                buttonAnswerController.SetLabel(buttonAnswerData.Data);
                buttonAnswerController.AudioClip = buttonAnswerData.audioClip;
                buttonAnswerController.IsCorrect = buttonAnswerData.IsCorect;
            }

            // notice init success
            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }

    }

    public class GameOneInitStateDependency
    {
        public QuestionLabelController QuestionController { get; set; }
        public ButtonSpeakerController ButtonSpeakerController { get; set; }
        public List<ButtonAnswerController> ButtonAnswerControllers { get; set; }
    }

    public class GameOneInitStateData
    {
        public string DataQuestion { get; set; }
        public AudioClip AudioClipQuesion { get; set; }
        public List<ButtonAnswerData> ListButtonAnswerDatas { get; set; }
    }

    public class ButtonAnswerData
    {
        public string Data { get; set; }
        public AudioClip audioClip { get; set; }
        public bool IsCorect { get; set; }
    }


}
