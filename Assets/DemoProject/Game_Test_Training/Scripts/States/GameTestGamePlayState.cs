using DG.Tweening;
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
        private float timer = 0f;
        private int index_Cage_Tiger = 0;
        private int index_Cage_Bear = 0;
        private const string ANIM_STAR = "4.0 - Sao";

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
            timer = 0;
            if (eventType.Data is AnimalButtonController)
            {
                animalButtonController = (AnimalButtonController)eventType.Data;
            }
            if (eventType.TypeEvent == AnswerChanel.Type.Pointer_Down)
            {
                Debug.LogError("OnPointerDown");
                animalButtonController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animkeo_loop, true);
                animalButtonController.SetScaleSkeleton(dependency.AnimalButtonConfig.SizeIncrease, true);

                SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxChoose);
                ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
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

                if(animalButtonController.IsCorrect)
                {
                    animalButtonController.SetScaleSkeleton(dependency.AnimalButtonConfig.SizeDecrease, true);
                   
                    if (animalButtonController.Cage_Type == (int)GameTestTypeCage.Cage_Bear)
                    {
                        animalButtonController.transform.DOMove(dependency.BearSnapPoints[index_Cage_Bear].transform.position, 0.2f);
                        index_Cage_Bear++;
                    } 
                    else
                    {
                        animalButtonController.transform.DOMove(dependency.TigerSnapPoints[index_Cage_Tiger].transform.position, 0.2f);
                        index_Cage_Tiger++;
                    }
                    animalButtonController.GetAnimStar().gameObject.SetActive(true);
                    animalButtonController.GetAnimStar().AnimationState.SetAnimation(0, ANIM_STAR, false).Complete += (trackEntry) => {
                        animalButtonController.GetAnimStar().gameObject.SetActive(false);
                    };

                    SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxCorrect);
                    ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
                }
                else
                {
                    SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxWrong);
                    ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
                }
            }
            else if (eventType.TypeEvent == AnswerChanel.Type.BeginDrag)
            {
                animalButtonController.transform.SetAsLastSibling();
                animalButtonController.GetSkeletonGraphic().AnimationState.SetAnimation(0, animConfig.animkeo_loop, true);
            }

            else if (eventType.TypeEvent == AnswerChanel.Type.OnDrag)
            {
                Debug.LogError("OnDrag");
                var skeletonGraphic = animalButtonController.GetSkeletonGraphic();
                var animationState = skeletonGraphic.AnimationState;

                animationState.TimeScale = 1.2f;
            }
        }
        public override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.GuidingStart);
                ObserverManager.TriggerEvent(stateChanel);
                timer = 0;
            }

            if(StaticValue.CountWrong == 3)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.GuidingStart);
                ObserverManager.TriggerEvent(stateChanel);
                StaticValue.CountWrong = 0;
            }

            if(index_Cage_Bear == 3 && index_Cage_Tiger ==3)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.PlayFinish);
                ObserverManager.TriggerEvent(stateChanel);
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
        public List<GameObject> BearSnapPoints { get; set; }
        public List<GameObject> TigerSnapPoints { get; set; }
    }
}
