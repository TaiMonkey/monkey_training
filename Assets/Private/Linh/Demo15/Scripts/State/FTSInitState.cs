
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monkey.Game.FTS
{
    public class FTSInitState : FSMState
    {
        private FTSInitStateDependency dependency;
        private FTSInitStateData stateData;
        public override void SetUp(object data)
        {
            dependency = (FTSInitStateDependency)data;
        }
        public override void OnEnter(object data)
        {
            stateData = (FTSInitStateData)data;
            dependency.ButtonShark.SetScale(Vector3.zero);
            List<FishnAnswerData> listFishAnswerDatas = stateData.ListFishAnsDatas;
            for (int count = 0; count < listFishAnswerDatas.Count; count++)
            {
                FishnAnswerData fishnAnswerData = listFishAnswerDatas[count];

                CaAnsController caAnsController = dependency.ListfishAns[count];

                caAnsController.SetLabel(fishnAnswerData.Data);
                caAnsController.AudioClip = fishnAnswerData.audioClip;
                caAnsController.IsCorrect = fishnAnswerData.IsCorect;
                caAnsController.SetScale(Vector3.zero);
            }

            // notice init success
            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }
    public class FTSInitStateDependency
    {
        public List<CaAnsController> ListfishAns { get; set; }
        public SharkController ButtonShark { get; set; }
    }
    public class FTSInitStateData
    {
        public string DataQuestion { get; set; }
        public List<FishnAnswerData> ListFishAnsDatas { get; set; }
    }

    public class FishnAnswerData
    {
        public string Data { get; set; }
        public AudioClip audioClip { get; set; }
        public bool IsCorect { get; set; }
    }
}
