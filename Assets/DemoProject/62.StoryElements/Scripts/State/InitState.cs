using System.Threading;
using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

namespace Monkey.Game.StoryElement
{
    public class InitState : FSMState
    {
        private InitStateDependency dependency;
        private CancellationTokenSource cts;

        public override void SetUp(object data)
        {
            dependency = (InitStateDependency)data;
        }

        public override async void OnEnter()
        {
            Debug.Log("InitState");
            SoundChannel soundChannel;
            cts = new();

            soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.InitConfig.Bg_audio);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            await UniTask.Delay(dependency.InitConfig.Delay500, cancellationToken: cts.Token);

            bool isFinishAudioCTA= false;
            soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.InitConfig.AudioCTA, () => { isFinishAudioCTA = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            await UniTask.WaitUntil(() => isFinishAudioCTA, cancellationToken: cts.Token);
            await UniTask.Delay(dependency.InitConfig.Delay500, cancellationToken: cts.Token);

            for(int i = 0; i < dependency.ListImageBackground.Count; i++)
            {
                dependency.ListImageBackground[i].DOFade(0, 1f).SetEase(Ease.Linear);
            }
        }
    }

    public class InitStateDependency
    {
        public InitConfig InitConfig { get; set; }
        public List<Image> ListImageBackground { get; set; } 
    }
}
