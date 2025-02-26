using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneIntroState : FSMState
    {
        private GameOneIntroStateDependency depency;
        private GameOneIntroStateData stateData;

        private CancellationTokenSource cts;
        private const float TIME_EFFECT = 0.5f;
        private const float TIME_DELAY = 0.2f;
        public override void SetUp(object data)
        {
            depency = (GameOneIntroStateDependency)data;
        }

        public override async void OnEnter(object data)
        {
            base.OnEnter(data);
            cts = new();
            stateData = (GameOneIntroStateData)data;

            bool isPlayFinishAudioIntro = false;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, stateData.AudioClip, () => { isPlayFinishAudioIntro = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

            await UniTask.WaitUntil(() => isPlayFinishAudioIntro, cancellationToken: cts.Token);


            depency.QuestionController.SetScale(Vector3.one, TIME_EFFECT);
            depency.ButtonSpeakerController.SetScale(Vector3.one, TIME_EFFECT, TIME_DELAY);
            List<ButtonAnswerController> listButtonAnswerControllers = depency.ButtonAnswerControllers;
            for(int count = 0; count < listButtonAnswerControllers.Count; count++)
            {
                listButtonAnswerControllers[count].SetScale(Vector3.one, TIME_EFFECT / 2);
            }

            float timeFinishIntroState = (TIME_EFFECT + TIME_DELAY) * 1000;
            await UniTask.Delay((int)timeFinishIntroState, cancellationToken: cts.Token);

            StateChanel stateChanel = new StateChanel(StateName.Status.IntroFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }

    public class GameOneIntroStateDependency
    {
        public QuestionLabelController QuestionController { get; set; }
        public ButtonSpeakerController ButtonSpeakerController { get; set; }
        public List<ButtonAnswerController> ButtonAnswerControllers { get; set; }
    }

    public class GameOneIntroStateData
    {
        public AudioClip AudioClip { get; set; }
    }

}
