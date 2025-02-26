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
        //private CancellationTokenSource cts;

        public void OnMMEvent(AnswerChanel eventType)
        {
            if(eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                Debug.LogError("Dang Click");
                if(currentButtonController != null)
                {
                    currentButtonController.ResetColor();
                }
                ButtonAnswerController buttonAnswerController = (ButtonAnswerController)eventType.Data;
                buttonAnswerController.SetColor();
                SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, buttonAnswerController.AudioClip);
                ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
                currentButtonController = buttonAnswerController;

                if(buttonAnswerController.IsCorrect)
                {
                    // anim phao hoa
                }
                else
                {
                    Debug.LogError("Errorrr");
                    StateChanel stateChanel = new StateChanel(StateName.Status.PlayEnd);
                    ObserverManager.TriggerEvent(stateChanel);
                }
            }
        }

        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
            //maxTurn = (int)data;
            Debug.LogError("GamePlay......");
            this.ObserverStartListening<AnswerChanel>();
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
