using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCPlayGameState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonAnsController currentButtonAnswerController;
        private float timer = 0f;
        private const int TIME_CHECK = 5;
        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                ButtonAnsController buttonAnsController = (ButtonAnsController)eventType.Data;
                CheckAnswer(buttonAnsController);
                Debug.LogError("CheckAnswer");
            }
        }
        public override void OnEnter(object data)
        {
            Debug.LogError("sssssssss");
            base.OnEnter(data);
            if (data != null)
            {
                ButtonAnsController buttonAnswerController = (ButtonAnsController)data;
                CheckAnswer(buttonAnswerController);
            }
            this.ObserverStartListening<AnswerChanel>();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            timer += Time.deltaTime; // Cộng dồn thời gian giữa mỗi frame

            if (timer >= TIME_CHECK) // Khi đủ 5 giây
            {
                timer = 0;
                StateChanel stateChanel = new StateChanel(StateName.Status.GuiddingStart);
                ObserverManager.TriggerEvent(stateChanel);
            }

        }

        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }
        private void CheckAnswer(ButtonAnsController buttonAnsController)
        {
            if (currentButtonAnswerController != null)
                currentButtonAnswerController.ResetColor();
            Debug.LogError(buttonAnsController.AudioClip.name);
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, buttonAnsController.AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            currentButtonAnswerController = buttonAnsController;
            currentButtonAnswerController.SetColor();
            bool isCorrect = buttonAnsController.IsCorrect;
            if (isCorrect)
            {
                
                StateChanel stateChanel = new StateChanel(StateName.Status.PlayFinish);
                ObserverManager.TriggerEvent(stateChanel);
            }
            else
            {
                

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
