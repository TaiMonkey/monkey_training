
using System.Threading;
using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneGamePlayState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonAnswerController currentButtonAnswerController;
        private int maxTurn;
        private const float TIME_DELAY_START = 1000;
        private CancellationTokenSource cts;

        public async void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                if (currentButtonAnswerController != null)
                    currentButtonAnswerController.ResetColor();

                ButtonAnswerController buttonAnswerController = (ButtonAnswerController)eventType.Data;
                buttonAnswerController.SetColor();
                SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, buttonAnswerController.AudioClip);
                ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
                currentButtonAnswerController = buttonAnswerController;

                bool isCorrect = buttonAnswerController.IsCorrect;
                if(isCorrect)
                {
                    cts = new();
                    await UniTask.Delay((int)TIME_DELAY_START, cancellationToken: cts.Token);
                    if (StaticValue.CurrentTurn < (maxTurn - 1))
                    {
                        // chuyen turn
                        StateChanel stateChanel = new StateChanel(StateName.Status.NexTurnStart);
                        ObserverManager.TriggerEvent(stateChanel);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("xxxx");
                        // end game
                    }
                }
                else
                {

                }
            }
        }

        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            maxTurn = (int)data;
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
