using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.CF
{
    public class CFPlayGameState : FSMState, EventListener<AnswerChanel>
    {
        private ButtonDomdomCotroller currentButtonDomDom;
        private ButtonJarController buttonJar;

        public void OnMMEvent(AnswerChanel eventType)
        {

            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                ButtonDomdomCotroller buttonDomdomCotroller = (ButtonDomdomCotroller)eventType.Data;
                ButtonJarController buttonJarController = (ButtonJarController)eventType.Data;
                CheckAnswer(buttonDomdomCotroller, buttonJarController);
                Debug.LogError("CheckAnswer");
            }
        }

        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }
        public override void OnEnter(object data)
        {
            
            base.OnEnter(data);
            if (data != null)
            {
                ButtonDomdomCotroller buttonDomdomCotroller = (ButtonDomdomCotroller)data;
                ButtonJarController buttonJarController = (ButtonJarController)data;
                CheckAnswer(buttonDomdomCotroller, buttonJarController);
            }
            this.ObserverStartListening<AnswerChanel>();
         


        }
        private void CheckAnswer(ButtonDomdomCotroller buttonDomdomCotroller, ButtonJarController buttonJarController)
        {

            if (currentButtonDomDom != null)
                currentButtonDomDom = buttonDomdomCotroller;
            buttonJar = buttonJarController;

            int isCorrect1 = buttonDomdomCotroller.IsCorrectID;
            int isCorrect2 = buttonJarController.IsCorrectID;
            if (isCorrect1 == isCorrect2)
            {
                buttonDomdomCotroller.transform.DOMove(buttonJar.transform.position, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                {
                    buttonDomdomCotroller.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear);
                };
                currentButtonDomDom.SetPositonOutScreen();
                currentButtonDomDom.DomDomMove();
            }
            else
            {
                currentButtonDomDom.OnBackButton(() => { });
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