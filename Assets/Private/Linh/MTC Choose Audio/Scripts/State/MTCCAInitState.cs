using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class MTCCAInitState : FSMState
    {
        private MTCCAInitStateDependency dependency;
        private MTCCAInitStateData stateData;
        public override void SetUp(object data)
        {
            dependency = (MTCCAInitStateDependency)data;
        }
        public override void OnEnter(object data)
        {
            stateData = (MTCCAInitStateData)data;

            dependency.ButtonQuestionController.SetImage(stateData.ImageQuestion);
            dependency.ButtonQuestionController.SetScale(Vector3.zero);
            dependency.ButtonQuestionController.AudioClip = stateData.AudioClipQuesion;
            dependency.BGContentController.SetScale(Vector3.zero);
            dependency.KhungAnsController.SetScale(Vector3.zero);
            List<ButtonAnswerData> listButtonAnswerDatas = stateData.ListButtonAnswerDatas;
            for (int count = 0; count < listButtonAnswerDatas.Count; count++)
            {
                ButtonAnswerData buttonAnswerData = listButtonAnswerDatas[count];
                ButtonAnsController buttonAnswerController = dependency.ButtonAnsControllers[count];
                buttonAnswerController.ReturnButtonToOriginalPosition();
                buttonAnswerController.AudioClip = buttonAnswerData.audioClip;
                buttonAnswerController.IsCorrect = buttonAnswerData.IsCorect;
                buttonAnswerController.SetScale(Vector3.zero);
              

            }

            // notice init success
            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }
    public class MTCCAInitStateDependency
    {
        public ButtonQuestionController ButtonQuestionController { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
        public KhungAnsController KhungAnsController { get; set; }
        public BGContentController BGContentController { get; set; }
    }
    public class MTCCAInitStateData
    {
        public Sprite ImageQuestion { get; set; }
        public AudioClip AudioClipQuesion { get; set; }
        public List<ButtonAnswerData> ListButtonAnswerDatas { get; set; }
    }

    public class ButtonAnswerData
    {
        public AudioClip audioClip { get; set; }
        public bool IsCorect { get; set; }
    }
}
