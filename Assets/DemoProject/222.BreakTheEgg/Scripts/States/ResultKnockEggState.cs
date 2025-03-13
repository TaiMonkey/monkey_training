using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using UnityEngine;
using UnityEngine.UI;

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
            dependency.ListImageAnswer[buttonEggController.Index].transform.parent.transform.DOMove(dependency.TargetPointImageAnswer.position, 1f).SetEase(Ease.OutBounce);

            await UniTask.Delay(dependency.GameplayConfig.Delay3000, cancellationToken: cts.Token);
            dependency.ListImageAnswer[buttonEggController.Index].transform.parent.transform.DOMove(dependency.PointOutScreen.position, 0.5f).SetEase(Ease.Linear);

            buttonEggController.transform.SetSiblingIndex(2);
            bool isMoveBackDone = false;
            buttonEggController.transform.DOMove(buttonEggController.OriginPos, 0.5f).SetEase(Ease.InOutQuad);
            buttonEggController.transform.DOScale(1, 0.5f).SetEase(Ease.InOutQuad)
                .OnComplete(() => {
                    buttonEggController.transform.SetSiblingIndex(1);
                    isMoveBackDone = true;
                });
            await UniTask.WaitUntil(() => isMoveBackDone, cancellationToken: cts.Token);
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
                buttonEggNextController.transform.DOMove(dependency.TargetPoint.position, 0.5f).SetEase(Ease.InOutQuad);
                buttonEggNextController.transform.DOScale(1.3f, 0.5f).SetEase(Ease.Linear);

                StateChanel stateChanel = new StateChanel(StateName.Status.ResultKnockEggFinish, buttonEggNextController);
                ObserverManager.TriggerEvent(stateChanel);
            } else
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.EndGameStart);
                ObserverManager.TriggerEvent(stateChanel);
            }
        }
    }

    public class ResultKnockEggStateDependency
    {
        public List<ButtonEggController> ButtonEggControllers { get; set; }
        public GameplayConfig GameplayConfig { get; set; }
        public ResultKnockEggConfig ResultKnockEggConfig { get; set; }
        public Transform TargetPoint { get; set; }
        public Transform TargetPointImageAnswer { get; set; }
        public Transform PointOutScreen { get; set; }
        public List<Image> ListImageAnswer { get; set; }
    }
}
