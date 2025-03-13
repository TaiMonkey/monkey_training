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
        private int numberClick = 0;
        private const int MAX_CLICK = 10;

        public override void SetUp(object data)
        {
            dependency = (GameplayStateDependency)data;
        }

        public override void OnEnter(object data)
        {
            Debug.LogError("Gameplay");
            currentButtonEggController = (ButtonEggController)data;

            this.ObserverStartListening<AnswerChanel>();
        }

        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Pointer_Down)
            {
                Debug.LogError("Pointer_Down");
                buttonEggController = (ButtonEggController)eventType.Data;

                if (currentButtonEggController == buttonEggController)
                {
                    currentButtonEggController.Isclicked = true;

                    numberClick++;
                    SkinName skinName = GetSkinName(currentButtonEggController.TypeEgg);
                    currentButtonEggController.SetAnimation(SetAnim(numberClick, skinName), false);
                }

                if (!currentButtonEggController.Isclicked)
                {
                    currentButtonEggController.transform.DOScale(1, 0.5f).SetEase(Ease.InOutQuad);
                    currentButtonEggController.transform.DOMove(currentButtonEggController.OriginPos, 0.5f)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            currentButtonEggController.SetLastSiblingImageFront();
                            currentButtonEggController = null;
                        });

                    buttonEggController.transform.SetAsLastSibling();
                    buttonEggController.transform.DOScale(1.3f, 0.5f).SetEase(Ease.InOutQuad);
                    buttonEggController.transform.DOMove(dependency.TargetPoint.position, 0.5f).SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            currentButtonEggController = buttonEggController;
                        }); 
                }

                if(numberClick == MAX_CLICK)
                {
                    numberClick = 0;
                    StateChanel stateChanel = new StateChanel(StateName.Status.PlayFinish, buttonEggController);
                    ObserverManager.TriggerEvent(stateChanel);
                }
            }
        }

        public SkinName GetSkinName(string TypeEgg)
        {
            switch (TypeEgg)
            {
                case "A":
                    return dependency.SkinConfig.EggA;
                case "B":
                    return dependency.SkinConfig.EggB;
                case "C":
                    return dependency.SkinConfig.EggC;
                default:
                    return null;
            }
        }

        public string SetAnim(int index, SkinName skinName)
        {
            switch(index)
            {
                case 1:
                    return skinName.Tap1;
                case 2:
                    return skinName.Tap2;
                case 3:
                    return skinName.Tap3;
                case 4:
                    return skinName.Tap4;
                case 5:
                    return skinName.Tap5;
                case 6:
                    return skinName.Tap6;
                case 7:
                    return skinName.Tap7;
                case 8:
                    return skinName.Tap8;
                case 9:
                    return skinName.Tap9;
                case 10:
                    return skinName.Tap10;

                default:
                    return "";
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
        public SkinConfig SkinConfig { get; set; }
        public GameplayConfig GameplayConfig { get; set; }
    }
}
