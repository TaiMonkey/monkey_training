using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using Spine.Unity;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestEndGameState : FSMState
    {
        private GameTestEndGameStateDependency dependency;
        private const string ANIM_STAR = "4.0 - Sao";

        public override void SetUp(object data)
        {
            dependency = (GameTestEndGameStateDependency)data;
        }

        public override void OnEnter()
        {
            DoWork(dependency.ButtonBearControllers);
            DoWork(dependency.ButtonTigerControllers);

            dependency.AnimStart.gameObject.SetActive(true);
            dependency.AnimStart.AnimationState.SetAnimation(0, ANIM_STAR, true);

            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.EndGameConfig.SfxCheer);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
        }

        private void DoWork(List<AnimalButtonController> buttons)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetSkeletonGraphic().AnimationState.SetAnimation(0, dependency.GameTestAnimalConfig.vui_mung, true);
            }
        }
    }

    public class GameTestEndGameStateDependency
    {
        public List<AnimalButtonController> ButtonBearControllers { get; set; }
        public List<AnimalButtonController> ButtonTigerControllers { get; set; }
        public SkeletonGraphic AnimStart { get; set; }
        public EndGameConfig EndGameConfig { get; set; }
        public GameTestAnimalConfig GameTestAnimalConfig { get; set; }
    }
}
