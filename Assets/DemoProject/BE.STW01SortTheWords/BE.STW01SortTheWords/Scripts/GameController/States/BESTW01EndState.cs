using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01EndState : FSMState
    {
        private BESTW01EndStateObjectDependency dependency;
        private CancellationTokenSource cts;

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            DoWork();
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01EndStateObjectDependency)data;
        }

        private async void DoWork()
        {
            cts = new CancellationTokenSource(); 
            SoundChannel soundData;
            try
            {
                dependency.EffectEndGame.Play();
                await UniTask.Delay(dependency.EndGameConfig.timeDelay, cancellationToken: cts.Token);
                bool tscSfx = false;

                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.EndGameConfig.sfxYeah, () => { tscSfx = true; });
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                await UniTask.WaitUntil(() => tscSfx, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.EndGameConfig.timeDelay * 4, cancellationToken: cts.Token);
                BESTW01HandleData.TriggerFinishState(BESTW01State.FinishGame, null);
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("Lucanhtai ex: " + ex);
            }
        }


        public override void OnExit()
        {
            base.OnExit();
            cts?.Cancel();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Cancel();
            cts?.Dispose();
        }
    }

    public class BESTW01EndStateObjectDependency
    {
        public BESTW01SEndGameConfig EndGameConfig { get; set; }
        public UIParticle UIParticle { get; set; }
        public ParticleSystem EffectEndGame { get; set; }
    }
}