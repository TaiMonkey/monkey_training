using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class MTCCADelayFinishState : FSMState
    {
        MTCCADelayFinishStateDependency dependency;
        private CancellationTokenSource cts;
        private const int TIME_DELAY = 2000;
        public override void SetUp(object data)
        {
            dependency = (MTCCADelayFinishStateDependency)data;
        }
        public override async void OnEnter(object data)
        {
            base.OnEnter(data);
            cts = new();
            dependency.PlaneAnimation.enabled = true;

          


            int maxTurn = (int)data;
            Debug.LogError(StaticValue.CurrentTurn + "CurrentTurn");
            Debug.LogError(maxTurn + "maxTurn");

            if (StaticValue.CurrentTurn == 0)
            {
                await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);

                StateChanel stateChanel = new StateChanel(StateName.Status.NexTurnStart);
                    ObserverManager.TriggerEvent(stateChanel);
                
            }
            else if (StaticValue.CurrentTurn == 1)
            {
                dependency.PlaneAnimation.AnimationState.SetAnimation(0, dependency.Anim.startingAnim4, false).Complete += (trackEntry) =>
                {
                    dependency.PlaneAnimation.freeze = true;
                    StateChanel stateChanel = new StateChanel(StateName.Status.NexTurnStart);
                    ObserverManager.TriggerEvent(stateChanel);
                };

            }
            else if (StaticValue.CurrentTurn == 2)
            {
                dependency.PlaneAnimation.AnimationState.SetAnimation(0, dependency.Anim.startingAnim6, false).Complete += (trackEntry) =>
                {
                    StateChanel stateChanel = new StateChanel(StateName.Status.NexTurnStart);
                    ObserverManager.TriggerEvent(stateChanel);
                };

            }
            else
            {
                UnityEngine.Debug.LogError("kkkkk");
                // end game
            }

           
        }
    }
    public class MTCCADelayFinishStateDependency
    {
        public SkeletonGraphic PlaneAnimation { get; set; }
        public Anim Anim { get; set; }
    }
}