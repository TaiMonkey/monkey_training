using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.GameTrainingDemo
{
    public class InitStateBuoi3 : FSMState
    {
        private InitStateBuoi3ObjectDependency dependency;
        private int count;
        public override void SetUp(object data)
        {
            Debug.LogError("zzzz");
            dependency = (InitStateBuoi3ObjectDependency)data;
            Debug.LogError(dependency.Buttons.Count);
        }
        public override void OnEnter(object data)
        {
            DataDemoBuoi3 dataDemoBuoi3 = (DataDemoBuoi3)data;

            Debug.LogError(dataDemoBuoi3.CurrenTurn.listDataButton[0].text);
            foreach (var fish in dependency.ListFish)
            {
                fish.transform.localScale = Vector3.zero;
                fish.IsEnable = false;
                fish.InitData(dependency.ImageOutline.GetComponent<RectTransform>());
            }
            MyMethod();
        }

        private void MyMethod()
        {
            Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.InitStateEnd, "sdfdsndsfsdjf");
            ObserverManager.TriggerEvent(buoi3Channer);
        }



        public override void OnExit()
        {
            base.OnExit();
        }

    }

    public class DataDemoBuoi3
    {
        public DemoTurn CurrenTurn { get; set; }
        public string DataEvent { get; set; }
    }

    public class InitStateBuoi3ObjectDependency
    {
        public List<DemoFishButton> ListFish { get; set; }
        public List<Button> Buttons { get; set; }
        public Image ImageOutline { get; set; }
    } 
}