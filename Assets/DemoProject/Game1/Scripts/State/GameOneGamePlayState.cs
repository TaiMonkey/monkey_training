
using System.Threading;
using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneGamePlayState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonAnswerController currentButtonAnswerController;
        private bool isCorrect = false;
        private ButtonAnswerController buttonAnswerController;
        private float timer = 0f;

        public async void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                buttonAnswerController = (ButtonAnswerController)eventType.Data;
                DoWork(buttonAnswerController);
            }
        }

        private void DoWork(ButtonAnswerController buttonAnswerController)
        {
            if (currentButtonAnswerController != null)
                currentButtonAnswerController.ResetColor();

            buttonAnswerController.SetColor();
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, buttonAnswerController.AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            currentButtonAnswerController = buttonAnswerController;

            isCorrect = buttonAnswerController.IsCorrect;
            if (isCorrect)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.PlayFinish);
                ObserverManager.TriggerEvent(stateChanel);
            }
            else
            {
            }
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
        }

        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter(object data)
        {
            if( data != null)
            {
                buttonAnswerController = (ButtonAnswerController)data;
                DoWork(buttonAnswerController);
            }
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
