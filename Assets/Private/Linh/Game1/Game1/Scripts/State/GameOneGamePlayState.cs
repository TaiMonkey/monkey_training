

using MonkeyBase.Observer;
using System.Threading.Tasks;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneGamePlayState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonAnswerController currentButtonAnswerController;
        private int maxTurn;
        private float timer = 0f;
        private const int TIME_CHECK = 5;
        public async void OnMMEvent(AnswerChanel eventType)
        {
            
            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                ButtonAnswerController buttonAnswerController = (ButtonAnswerController)eventType.Data;
                CheckAnswer(buttonAnswerController);
                Debug.LogError("CheckAnswer");
            }
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

        public override void OnEnter(object data)
        {
            Debug.LogError("sssssssss");
            base.OnEnter(data);
            if(data != null)
            {
                ButtonAnswerController buttonAnswerController = (ButtonAnswerController)data;
                CheckAnswer(buttonAnswerController);
            }
            this.ObserverStartListening<AnswerChanel>();
            timer = 0;

        }

        private void CheckAnswer(ButtonAnswerController buttonAnswerController)
        {
            if (currentButtonAnswerController != null)
                currentButtonAnswerController.ResetColor();
            Debug.LogError(buttonAnswerController.AudioClip.name);
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, buttonAnswerController.AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            currentButtonAnswerController = buttonAnswerController;
            currentButtonAnswerController.SetColor();
            bool isCorrect = buttonAnswerController.IsCorrect;
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
