using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    public class GameplayState : FSMState, EventListener<AnswerChanel>
    {
        private GameplayStateDependency dependency;
        private ButtonEggController buttonEggController;
        private ButtonEggController currentButtonEggController;

        public override void SetUp(object data)
        {
            dependency = (GameplayStateDependency)data;
        }

        public override void OnEnter(object data)
        {
            currentButtonEggController = (ButtonEggController)data;

            this.ObserverStartListening<AnswerChanel>();
        }

        public void OnMMEvent(AnswerChanel eventType)
        {
            buttonEggController = (ButtonEggController)eventType.Data;

            if (eventType.TypeEvent == AnswerChanel.Type.Pointer_Down)
            {
                Debug.LogError("âvbasvasv");
                if (currentButtonEggController == buttonEggController)
                {
                    currentButtonEggController.Isclicked = true;
                }
                if (!currentButtonEggController.Isclicked)
                {
                    currentButtonEggController.transform.DOMove(currentButtonEggController.OriginPos.position, 0.5f).SetEase(Ease.InOutQuad);
                    //buttonEggController.transform.DOMove(dependency.TargetPoint.position, 0.5f).SetEase(Ease.InOutQuad);
                   // currentButtonEggController = buttonEggController;
                }
                else
                {
                    Debug.LogError(1);
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            this.ObserverStopListening<AnswerChanel>();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<AnswerChanel>();
        }
    }

    public class GameplayStateDependency
    {
        public Transform TargetPoint { get; set; }
    }
}
