using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCIntroState : FSMState
    {
        private MTCIntroStateDependency dependency;
        private MTCIntroStateData stateData;
        private CancellationTokenSource cts;
        private const float TIME_EFFECT = 0.5f;
        private const float TIME_DELAY = 0.2f;
        public override void SetUp(object data)
        {
            dependency = (MTCIntroStateDependency)data;
        }
        public override async void OnEnter(object data)
        {
            base.OnEnter(data);
            cts = new();
            stateData = (MTCIntroStateData)data;
           
            bool isPlayFinishAudioIntro = false;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, stateData.AudioClip, () => { isPlayFinishAudioIntro = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            Debug.LogError(stateData.AudioClip.name + "");
            await UniTask.WaitUntil(() => isPlayFinishAudioIntro, cancellationToken: cts.Token);


            dependency.LabelQuestionController.SetScale(Vector3.one, TIME_EFFECT);
            dependency.ButtonSpeakerController.SetScale(Vector3.one, TIME_EFFECT, TIME_DELAY);
            List<ButtonAnsController> listButtonAnswerControllers = dependency.ButtonAnsControllers;
            for (int count = 0; count < listButtonAnswerControllers.Count; count++)
            {
                listButtonAnswerControllers[count].SetScale(Vector3.one, TIME_EFFECT / 2);
            }

            float timeFinishIntroState = (TIME_EFFECT + TIME_DELAY) * 1000;
            await UniTask.Delay((int)timeFinishIntroState, cancellationToken: cts.Token);

            StateChanel stateChanel = new StateChanel(StateName.Status.IntroFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }

    public class MTCIntroStateDependency
    {
        public LabelQuestionController LabelQuestionController { get; set; }
        public ButtonSpeakController ButtonSpeakerController { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
    }

    public class MTCIntroStateData
    {
        public AudioClip AudioClip { get; set; }
    }
}
