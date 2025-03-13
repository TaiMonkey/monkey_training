using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCDelayFinishState : FSMState
    {
        private MTCDelayFinishStateDependency dependency;
        private CancellationTokenSource cts;
        private int maxTurn;
        private const int TIME_DELAY = 2000;
      
        public override void SetUp(object data)
        {
            dependency = (MTCDelayFinishStateDependency)data;
        }
        public override async void OnEnter(object data)
        {
            maxTurn = (int)data;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            cts = new();
            await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);
  

            if (StaticValue.CurrentTurn < (maxTurn - 1))
            {
                Debug.LogError("DelayState");
                // chuyen turn
                StaticValue.CurrentTurn++;
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
        public class MTCDelayFinishStateDependency
        {
            public AudioClip AudioClip { get; set; }
        }
}
