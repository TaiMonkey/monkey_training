using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    public class Game2GamePlayState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonAnswerController currentButtonController;
        private float timer = 0f;

        public void OnMMEvent(AnswerChanel eventType)
        {
            if(eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                Debug.LogError("Dang Click");
                ButtonAnswerController buttonAnswerController = (ButtonAnswerController)eventType.Data;
                DoWork(buttonAnswerController);
            }
        }

        private async void DoWork(ButtonAnswerController buttonAnswerController)
        {
            if (currentButtonController != null)
            {
                currentButtonController.ResetColor();
            }
            buttonAnswerController.SetColor();
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, buttonAnswerController.AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            currentButtonController = buttonAnswerController;

            if (buttonAnswerController.IsCorrect)
            {
                // anim phao hoa
                buttonAnswerController.animState.AnimationState.SetAnimation(0, "Phao hoa 1", false);
                // chuyen state
                StateChanel stateChanel = new StateChanel(StateName.Status.PlayEnd);
                ObserverManager.TriggerEvent(stateChanel);
            }
            else
            {
                await buttonAnswerController.AnimWrong();
                buttonAnswerController.ResetColor();
            }
        }

        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter(object data)
        {
            if(data != null)
            {
                ButtonAnswerController buttonAnswerController = (ButtonAnswerController)data;
                DoWork(buttonAnswerController);
            }
            this.ObserverStartListening<AnswerChanel>();
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.GuidingStart);
                ObserverManager.TriggerEvent(stateChanel);
                timer = 0;
            }
            if(StaticValue.CountWrong == 3)
            {
                StaticValue.CountWrong = 0;
                StateChanel stateChanel = new StateChanel(StateName.Status.GuidingStart);
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
}
