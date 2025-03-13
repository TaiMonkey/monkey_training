using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace Monkey.Game.MTCCA
{

    public class MTCCAPlayState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonAnsController currentButtonAnsController;
        public CancellationTokenSource cts;
        private MTCCAPlayStateDependency dependency;
        private float timer = 0f;
        private const int TIME_CHECK = 5;





        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.Data is ButtonAnsController)
            {
                currentButtonAnsController = (ButtonAnsController)eventType.Data;
            }
            if (eventType.TypeEvent == AnswerChanel.Type.Click)
            {
                Debug.LogError("Click");

            }
            else if (eventType.TypeEvent == AnswerChanel.Type.OnDrag)
            {
              Debug.LogError("OnDrag");
               
            }
            else if (eventType.TypeEvent == AnswerChanel.Type.EndDrag)
            {
                Debug.LogError("EndDrag");
                CheckAnswer(currentButtonAnsController);
            }
        }

        public override void SetUp(object data)
        {
            dependency = (MTCCAPlayStateDependency)data;
        }
        public override void OnEnter(object data)
        {
            Debug.LogError("OnEnter MTCCAPlayState");
            base.OnEnter(data);
            if (data != null)
            {
                ButtonAnsController buttonAnsController = (ButtonAnsController)data;
                CheckAnswer(buttonAnsController);
            }
            this.ObserverStartListening<AnswerChanel>();
            timer = 0;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            timer += Time.deltaTime; 

            if (timer >= TIME_CHECK) 
            {
                timer = 0;
                StateChanel stateChanel = new StateChanel(StateName.Status.GuiddingStart);
                ObserverManager.TriggerEvent(stateChanel);
            }

        }
        private async void CheckAnswer(ButtonAnsController buttonAnsController)
        {
           
            cts = new();
            if (buttonAnsController != null)
                buttonAnsController.ResetColor();
                buttonAnsController.SetColor();
            bool isCorrect = buttonAnsController.IsCorrect;
            bool moveComplete = false;
            buttonAnsController.transform.DOMove(dependency.TransImage.position, 0.5f).SetEase(Ease.Linear).onComplete += () =>
            {
                moveComplete = true;
                
            };
            await UniTask.WaitUntil(() => moveComplete, cancellationToken: cts.Token);
            if (isCorrect)
            {

                    buttonAnsController.ResetColor();
                dependency.KhungAnsController.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete += () => 
                {
                    buttonAnsController.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.Linear).onComplete += () =>
                    {
                    buttonAnsController.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete += () =>{};
                    };
                };
                StateChanel stateChanel = new StateChanel(StateName.Status.PlayFinish);
                ObserverManager.TriggerEvent(stateChanel);

            }
            else
            {
                 buttonAnsController.OnActionWrong();
                 buttonAnsController.ResetColor();
                 buttonAnsController.ReturnButtonToOriginalPosition();
            }
            moveComplete = false;
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
    public class MTCCAPlayStateDependency
    {
        public Transform TransImage { get; set; }
        public KhungAnsController KhungAnsController { get; set; }

    }
}
