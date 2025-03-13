using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    public class GuidingState : FSMState, EventListener<AnswerChanel>
    {
        private GuidingStateDependency dependency;
        private CancellationTokenSource cts;
        private const int TIME_DELAY_START = 11000;
        private bool isGuiding = false;

        public override void SetUp(object data)
        {
            dependency = (GuidingStateDependency)data;
        }

        public override async void OnEnter()
        {
            Debug.LogError("Guiding");
            this.ObserverStartListening<AnswerChanel>();
            cts = new();
            isGuiding = true;
            try
            {
                while (isGuiding)
                {
                    SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.GuidingConfig.SfxGuiding);
                    ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

                    await UniTask.Delay(TIME_DELAY_START, cancellationToken: cts.Token);
                }
            }
            catch (OperationCanceledException ex)
            {
                Debug.Log(ex);
            }
        }

        public void OnMMEvent(AnswerChanel eventType)
        {
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PAUSE_SOUND, dependency.GuidingConfig.SfxGuiding);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

            StateChanel stateChanel = new StateChanel(StateName.Status.GuidingFinish, eventType.Data);
                ObserverManager.TriggerEvent(stateChanel);
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

    public class GuidingStateDependency
    {
        public GuidingConfig GuidingConfig { get; set; }
    }
}
