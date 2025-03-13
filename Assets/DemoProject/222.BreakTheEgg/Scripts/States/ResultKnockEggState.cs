using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    public class ResultKnockEggState : FSMState
    {
        private ResultKnockEggStateDependency dependency;
        private ButtonEggController buttonEggController;
        private ButtonEggController buttonEggNextController;
        private CancellationTokenSource cts;

        public override void SetUp(object data)
        {
            dependency = (ResultKnockEggStateDependency)data;
        }

        public override async void OnEnter(object data)
        {
            Debug.LogError("ResultKnockEggState");
            cts = new();
            buttonEggController = (ButtonEggController)data;

            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.GameplayConfig.SfxTextShow);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

            buttonEggController.SetActiveFirework(true);
            buttonEggController.SetAnimationFirework();
            buttonEggController.GetTextAnswer().transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    buttonEggController.GetTextAnswer().transform.DOScale(1f, 1f).SetEase(Ease.InOutQuad);
                });
            await UniTask.Delay(dependency.GameplayConfig.Delay3000, cancellationToken: cts.Token);

            buttonEggController.transform.SetSiblingIndex(2);
            buttonEggController.transform.DOMove(buttonEggController.OriginPos, 0.5f).SetEase(Ease.InOutQuad);
            buttonEggController.transform.DOScale(1, 0.5f).SetEase(Ease.InOutQuad)
                .OnComplete(() => {
                    buttonEggController.transform.SetSiblingIndex(1);
                });

            HanldeNextEgg();
        }

        public void HanldeNextEgg()
        {
            for(int i = 0; i < dependency.ButtonEggControllers.Count; i ++)
            {
                if(dependency.ButtonEggControllers[i].Isclicked)
                {
                    dependency.ButtonEggControllers.RemoveAt(i);
                }
            }
            if (dependency.ButtonEggControllers.Count > 0)
            {
                int index = Random.Range(0, dependency.ButtonEggControllers.Count);
                buttonEggNextController = dependency.ButtonEggControllers[index];
                buttonEggNextController.transform.SetAsLastSibling();
                buttonEggNextController.transform.DOMove(dependency.TargetPoint.position, 1f).SetEase(Ease.InOutQuad);
                buttonEggNextController.transform.DOScale(1.3f, 0.5f).SetEase(Ease.Linear);

                StateChanel stateChanel = new StateChanel(StateName.Status.ResultKnockEggFinish, buttonEggNextController);
                ObserverManager.TriggerEvent(stateChanel);
            } else
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.EndGameStart, buttonEggNextController);
                ObserverManager.TriggerEvent(stateChanel);
            }
        }
    }

    public class ResultKnockEggStateDependency
    {
        public List<ButtonEggController> ButtonEggControllers { get; set; }
        public GameplayConfig GameplayConfig { get; set; }
        public Transform TargetPoint { get; set; }
    }
}
