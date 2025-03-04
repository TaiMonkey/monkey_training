using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01IntroState : FSMState
    {
        private BESTW01IntroStateObjectDependency dependency;
        private CancellationTokenSource cts;

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            DoWork();
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01IntroStateObjectDependency)data;
        }

        private async void DoWork()
        {
            cts = new CancellationTokenSource();
            SoundChannel soundData;
            try
            {
                dependency.Carousel.EnableMovingConveyor(true);
                await UniTask.Delay(dependency.IntroConfig.timeDelay, cancellationToken: cts.Token);
                bool tscAudio = false;

                int randomIndex = UnityEngine.Random.RandomRange(0, dependency.IntroConfig.audiosTopic.Length);
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.IntroConfig.audiosTopic[randomIndex], () => { tscAudio = true; }, 1f);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
              
                await UniTask.WaitUntil(() => tscAudio, cancellationToken: cts.Token);
                dependency.Carousel.EnableMovingCarousel(true);
                BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
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

  
    public class BESTW01IntroStateObjectDependency
    {
        public BESTW01IntroConfig IntroConfig { get; set; }
        public BESTW01Carousel Carousel { get; set; }
        public List<BESTW01CardItem> CardItems { get; set; }
    }
}