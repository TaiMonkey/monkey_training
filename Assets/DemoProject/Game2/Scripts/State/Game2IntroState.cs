using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    public class Game2IntroState : FSMState
    {
        private Game2IntroStateDependency dependency;
        private Game2IntroStateData introStateData;

        private CancellationTokenSource cts;
        private const float TIME_DELAY = 0.2f;

        public override void SetUp(object data)
        {
            dependency = (Game2IntroStateDependency)data;
        }

        public override async void OnEnter(object Data)
        {
            introStateData = (Game2IntroStateData)Data;
            cts = new();

            // Play audio question
            bool isPlayFinishAudioIntro = false;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, introStateData.AudioClip, () => { isPlayFinishAudioIntro = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            await UniTask.WaitUntil(() => isPlayFinishAudioIntro, cancellationToken: cts.Token);

            List<ButtonAnswerController> buttonAnswerControllers = dependency.ButtonAnswers;
            bool isPopup = false;
            for(int i = 0; i <buttonAnswerControllers.Count; i++)
            {
                buttonAnswerControllers[i].transform.DOScale(Vector3.one, TIME_DELAY).SetEase(Ease.Linear).onComplete += () =>
                {
                    isPopup = true;
                };
                await UniTask.WaitUntil(() => isPopup, cancellationToken: cts.Token);
                isPopup = false;
            }

            StateChanel stateChanel = new StateChanel(StateName.Status.IntroEnd);
            ObserverManager.TriggerEvent(stateChanel);
        }

    }

    public class Game2IntroStateDependency
    {
        public List<ButtonAnswerController> ButtonAnswers { get; set; }
    }

    public class Game2IntroStateData
    {
        public AudioClip AudioClip { get; set; }
    }


}


