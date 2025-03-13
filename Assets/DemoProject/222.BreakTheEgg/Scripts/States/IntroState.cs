using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.BreakTheEgg
{
    public class IntroState : FSMState
    {
        private IntroStateDependency dependency;
        private CancellationTokenSource cts;
        private ButtonEggController buttonEggController;

        public override void SetUp(object data)
        {
            dependency = (IntroStateDependency)data;
        }

        public override async void OnEnter()
        {
            cts = new();
            SoundChannel soundChannel;
            Debug.LogError("IntroState");

            for (int i = 0; i < dependency.ButtonEggControllers.Count; i++)
            {
                ButtonEggController buttonEgg = dependency.ButtonEggControllers[i];
                buttonEgg.gameObject.SetActive(true);
            }
            ActiveObject(dependency.ListImageBack);
            ActiveObject(dependency.ListImageFront);
            await UniTask.Delay(dependency.IntroConfig.TimeDelay, cancellationToken: cts.Token);

            int index = Random.Range(0, dependency.ButtonEggControllers.Count);
            buttonEggController = dependency.ButtonEggControllers[index];
            buttonEggController.transform.SetAsLastSibling();
            Debug.LogError("SetLastSiblingImageFront-intro");
            //buttonEggController.SetAlphaBackgroundText(true);
            //buttonEggController.GetAlphabetAnswer().transform.localScale = Vector3.zero;
            buttonEggController.transform.DOMove(dependency.TargetPoint.position, 1f).SetEase(Ease.InOutQuad);
            buttonEggController.transform.DOScale(1.3f, 0.5f).SetEase(Ease.Linear);

            bool isFinishAudioCTA = false;
            soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.IntroConfig.SfxJump, () => { isFinishAudioCTA = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

            soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.IntroConfig.InstructionCTA);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

            StateChanel stateChanel = new StateChanel(StateName.Status.IntroFinish, buttonEggController);
            ObserverManager.TriggerEvent(stateChanel);
        }

        private void ActiveObject(List<Image> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].gameObject.SetActive(true);
            }
        }
    }

    public class IntroStateDependency
    {
        public List<ButtonEggController> ButtonEggControllers { get; set; }
        public IntroConfig IntroConfig { get; set; }
        public Transform TargetPoint { get; set; }
        public List<Image> ListImageBack { get; set; }
        public List<Image> ListImageFront { get; set; }
    }
}
