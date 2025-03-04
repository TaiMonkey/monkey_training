using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestInitState : FSMState
    {
        public GameTestInitStateDependency dependency;

        private CancellationTokenSource cts;
        private const int TIME_DELAY = 1000;

        public override void SetUp(object data)
        {
            dependency = (GameTestInitStateDependency)data;
        }

        public override async void OnEnter(object data)
        {
            cts = new();
            Debug.LogError("Init");
            for(int i = 0; i< dependency.ButtonBearControllers.Count; i ++)
            {
                AnimalButtonController buttonBearController = dependency.ButtonBearControllers[i];
                buttonBearController.SetAnimStart(dependency.AnimConfig.animNomal);
                buttonBearController.InitDataCage(dependency.Cage_Bear);
                buttonBearController.OriginPos = buttonBearController.transform.position;
            }
            for (int i = 0; i < dependency.ButtonTigerControllers.Count; i++)
            {
                AnimalButtonController buttonTigerController = dependency.ButtonTigerControllers[i];
                buttonTigerController.SetAnimStart(dependency.AnimConfig.animNomal);
                buttonTigerController.InitDataCage(dependency.Cage_Tiger);
                buttonTigerController.OriginPos = buttonTigerController.transform.position;
            }
            //dependency.Cage_Bear.Cage_type = (int)GameTestTypeCage.Cage_Bear;
            //dependency.Cage_Tiger.Cage_type = (int)GameTestTypeCage.Cage_Tiger;

            await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);

            bool isPlayFinishAudioIntro = false;
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AudioCTA, () => { isPlayFinishAudioIntro = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            await UniTask.WaitUntil(() => isPlayFinishAudioIntro, cancellationToken: cts.Token);

            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);
        }
    }

    public class GameTestInitStateDependency
    {
        public AudioClip AudioCTA { get; set; }
        public List<AnimalButtonController> ButtonBearControllers { get; set; }
        public List<AnimalButtonController> ButtonTigerControllers { get; set; }
        public RectTransform Cage_Tiger { get; set; }
        public RectTransform Cage_Bear { get; set; }
        public GameTestAnimalConfig AnimConfig { get; set; }
     }
}
