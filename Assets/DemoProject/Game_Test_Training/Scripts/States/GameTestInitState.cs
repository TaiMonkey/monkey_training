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
                ButtonBearController buttonBearController = dependency.ButtonBearControllers[i];
                buttonBearController.SetAnimStart(dependency.AnimConfig.animNomal);
            }
            for (int i = 0; i < dependency.ButtonTigerControllers.Count; i++)
            {
                ButtonTigerController buttonTigerController = dependency.ButtonTigerControllers[i];
                buttonTigerController.SetAnimStart(dependency.AnimConfig.animNomal);
            }
            dependency.Cage_Bear.Cage_type = (int)GameTestTypeCage.Cage_Bear;
            dependency.Cage_Tiger.Cage_type = (int)GameTestTypeCage.Cage_Tiger;

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
        public List<ButtonBearController> ButtonBearControllers { get; set; }
        public List<ButtonTigerController> ButtonTigerControllers { get; set; }
        public CageController Cage_Tiger { get; set; }
        public CageController Cage_Bear { get; set; }
        public GameTestAnimalConfig AnimConfig { get; set; }
     }
}
