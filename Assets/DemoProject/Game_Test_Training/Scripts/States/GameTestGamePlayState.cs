using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestGamePlayState : FSMState, EventListener<AnswerChanel>
    {
        private GameTestGamePlayStateDependency dependency;
        private AnimalButtonController animalButtonController;
        private GameTestAnimalConfig animConfig;

        public override void SetUp(object data)
        {
            dependency = (GameTestGamePlayStateDependency)data;
        }

        public override void OnEnter()
        {
            Debug.LogError("GamePlay");
            animConfig = dependency.AnimConfig;
            this.ObserverStartListening<AnswerChanel>();
        }

        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.Data is AnimalButtonController)
            {
                animalButtonController = (AnimalButtonController)eventType.Data;
            }
            if (eventType.TypeEvent == AnswerChanel.Type.Pointer_Down)
            {
                Debug.LogError("OnPointerDown");
                //DoWorkButton(dependency.BearButtonControllers);
                //DoWorkButton(dependency.TigerButtonControllers);

                animalButtonController.SetScaleSkeleton(dependency.AnimalButtonConfig.SizeIncrease, true);
                SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxChoose);
                ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

                animalButtonController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animkeo_loop, true);
            }
            else if (eventType.TypeEvent == AnswerChanel.Type.Pointer_Up)
            {
                Debug.LogError("OnPointerUp");
                animalButtonController.GetSkeletonGraphic().AnimationState.TimeScale = 1f;
                animalButtonController.SetScaleSkeleton(dependency.AnimalButtonConfig.SizeIncrease, false);
                animalButtonController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.tha, false).Complete += (trackEntry) =>
                {
                    animalButtonController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animNomal, true);
                };
                animalButtonController.IsEnable = true;
            }
            else if (eventType.TypeEvent == AnswerChanel.Type.BeginDrag)
            {
                //DoWorkButton(dependency.BearButtonControllers);
                //DoWorkButton(dependency.TigerButtonControllers);
                animalButtonController.transform.SetAsLastSibling();
                animalButtonController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animkeo_loop, true);
            }

            else if (eventType.TypeEvent == AnswerChanel.Type.OnDrag)
            {
                Debug.LogError("OnDrag");
                var skeletonGraphic = animalButtonController.GetSkeletonGraphic();
                var animationState = skeletonGraphic.AnimationState;

                animationState.TimeScale = 1.2f;

                // Chạy animation
                //animationState.SetAnimation(0, animConfig.animkeo_loop, true);
            }
        }

        private void DoWorkButton(List<AnimalButtonController> animalButtonControllers)
        {
            foreach (var item in animalButtonControllers)
            {
                if (item != animalButtonController)
                {
                    item.IsEnable = false;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            this.ObserverStopListening<AnswerChanel>();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<AnswerChanel>();
        }
    }
    public class GameTestGamePlayStateDependency
    {
        public AnimalButtonConfig AnimalButtonConfig { get; set; }
        public GameTestAnimalConfig AnimConfig { get; set; }
        public List<AnimalButtonController> TigerButtonControllers { get; set; }
        public List<AnimalButtonController> BearButtonControllers { get; set; }
    }
}
