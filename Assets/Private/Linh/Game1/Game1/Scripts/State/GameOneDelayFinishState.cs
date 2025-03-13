using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.Game.GameOneDemo
{
    public class GameOneDelayFinishState : FSMState
    {
        private GameOneDelayFinishDependency dependency;
        private CancellationTokenSource cts;
        private const int TIME_DELAY = 2000;
        public override void SetUp(object data)
        {
            dependency = (GameOneDelayFinishDependency)data;
        }
        public override async void OnEnter(object data)
        {
            base.OnEnter(data);
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            cts = new();
            await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);
            int maxTurn = (int)data;

            if (StaticValue.CurrentTurn < (maxTurn - 1))
            {
                Debug.LogError("DelayState");
                // chuyen turn
                StateChanel stateChanel = new StateChanel(StateName.Status.NexTurnStart);
                ObserverManager.TriggerEvent(stateChanel);
            }
            else
            {
                UnityEngine.Debug.LogError("xxxx");
                // end game
            }



        }
    }
    public class GameOneDelayFinishDependency
    {
        public AudioClip AudioClip { get; set; }
    }
}