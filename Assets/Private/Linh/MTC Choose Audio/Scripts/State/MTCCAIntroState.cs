using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.Game.MTCCA {
    public class MTCCAIntroState : FSMState
    {
        private MTCCAIntroStateDependency dependency;
        private MTCCAIntroStateData stateData;

        private CancellationTokenSource cts;
        private const float TIME_EFFECT = 0.5f;
        private const float TIME_DELAY = 0.2f;

        public override void SetUp(object data)
        {
            dependency = (MTCCAIntroStateDependency)data;
        }
        public override async void OnEnter(object data)
        {
            base.OnEnter(data);
            cts = new();
            stateData = (MTCCAIntroStateData)data;

            /*bool isPlayFinishAudioIntro = false;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, stateData.AudioClip, () => { isPlayFinishAudioIntro = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            
            await UniTask.WaitUntil(() => isPlayFinishAudioIntro, cancellationToken: cts.Token);*/


            //dependency.ButtonQuestionController.SetScale(Vector3.one, TIME_EFFECT);
            dependency.BGContentController.SetScale(Vector3.one, TIME_EFFECT);

            List<ButtonAnsController> listButtonAnswerControllers = dependency.ButtonAnsControllers;
            for (int count = 0; count < listButtonAnswerControllers.Count; count++)
            {
                listButtonAnswerControllers[count].ChangeImageColor("#AFAFAF");
                listButtonAnswerControllers[count].SetScale(Vector3.one, TIME_EFFECT);
            }

            bool isPlayAudioQues = false;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.ButtonQuestionController.AudioClip, () => { isPlayAudioQues = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            await UniTask.WaitUntil(() => isPlayAudioQues, cancellationToken: cts.Token);
            dependency.ButtonQuestionController.SetScale(Vector3.one, TIME_EFFECT);
            

            bool isPlayFinishAudioIntro = false;
            SoundChannel soundChannel1 = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, stateData.AudioClip, () => { isPlayFinishAudioIntro = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel1);
            await UniTask.WaitUntil(() => isPlayFinishAudioIntro, cancellationToken: cts.Token);


            listButtonAnswerControllers[0].ChangeImageColor("#48CBFF");
            bool isPlayAudio1 = false;
            SoundChannel soundChannel2 = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, listButtonAnswerControllers[0].AudioClip, () => { isPlayAudio1 = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel2);
            await UniTask.WaitUntil(() => isPlayAudio1, cancellationToken: cts.Token);

            listButtonAnswerControllers[1].ChangeImageColor("#48CBFF");
            bool isPlayAudio2 = false;
            SoundChannel soundChannel3 = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, listButtonAnswerControllers[1].AudioClip, () => { isPlayAudio2 = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel3);
            await UniTask.WaitUntil(() => isPlayAudio2, cancellationToken: cts.Token);

            listButtonAnswerControllers[2].ChangeImageColor("#48CBFF");
            bool isPlayAudio3 = false;
            SoundChannel soundChannel4 = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, listButtonAnswerControllers[2].AudioClip, () => { isPlayAudio3 = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel4);
            await UniTask.WaitUntil(() => isPlayAudio2, cancellationToken: cts.Token);

            dependency.KhungAnsController.SetScale(Vector3.one, TIME_EFFECT);

            float timeFinishIntroState = (TIME_EFFECT + TIME_DELAY) * 1000;
            await UniTask.Delay((int)timeFinishIntroState, cancellationToken: cts.Token);

            StateChanel stateChanel = new StateChanel(StateName.Status.IntroFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }
    public class MTCCAIntroStateDependency
    {
        public ButtonQuestionController ButtonQuestionController { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
        public MTCCAIntroConfig MTCCAIntroConfig { get; set; }
        public KhungAnsController KhungAnsController { get; set; }
        public BGContentController BGContentController { get; set;   }
    }

    public class MTCCAIntroStateData
    {
        public AudioClip AudioClip { get; set; }
    }
}
