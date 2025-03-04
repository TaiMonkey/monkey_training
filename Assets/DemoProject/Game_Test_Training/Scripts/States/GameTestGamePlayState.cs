using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestGamePlayState : FSMState, EventListener<AnswerChanel>
    {
        private GameTestGamePlayStateDependency dependency;
        private ButtonBearController buttonBearController;
        private ButtonTigerController buttonTigerController;
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
            if(eventType.TypeEvent == AnswerChanel.Type.Pointer_Down)
            {
                if(eventType.Data is ButtonBearController)
                {
                    Debug.LogError("OnPointerDown");
                    buttonBearController = (ButtonBearController)eventType.Data;
                    buttonBearController.SetScaleSpine(dependency.AnimalButtonConfig.SizeIncrease, true);
                    SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxChoose);
                    ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

                    buttonBearController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animkeo_loop, true);
                }
                else
                {
                    buttonTigerController = (ButtonTigerController)eventType.Data;
                    buttonTigerController.SetScaleSpine(dependency.AnimalButtonConfig.SizeIncrease, true);
                    SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxChoose);
                    ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

                    buttonTigerController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animkeo_loop, true);
                }
            }
            else if (eventType.TypeEvent == AnswerChanel.Type.Pointer_Up)
            {

                if (eventType.Data is ButtonBearController)
                {
                    Debug.LogError("OnPointerUp");
                    buttonBearController = (ButtonBearController)eventType.Data;
                    buttonBearController.SetScaleSpine(dependency.AnimalButtonConfig.SizeIncrease, false);
                    buttonBearController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.tha, false).Complete += (trackEntry) =>
                    {
                        buttonBearController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animNomal, true);
                    };
                }
                else
                {
                    buttonTigerController = (ButtonTigerController)eventType.Data;
                    buttonTigerController.SetScaleSpine(dependency.AnimalButtonConfig.SizeIncrease, false);
                    buttonTigerController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.tha, false).Complete += (trackEntry) =>
                    {
                        buttonTigerController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animNomal, true);
                    };
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
    }
}
