using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTCCA
{
    public class MTCCAGuidingState : FSMState, EventListener<AnswerChanel>
    {
        private MTCCAGuidingStateDependency dependency;
        private CancellationTokenSource cts;
        private ButtonAnsController buttonCorrect;
        private ButtonAnsController tempButtonCorrect;
        private bool isGuiding = false;

        public override void SetUp(object data)
        {
            dependency = (MTCCAGuidingStateDependency)data;
        }
        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Click|| eventType.TypeEvent == AnswerChanel.Type.OnDrag)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.GuiddingFinish, eventType.Data);
                ObserverManager.TriggerEvent(stateChanel);
            }
        }
        public override void OnEnter(object data)
        {
            base.OnEnter();
            cts = new CancellationTokenSource();
           
            this.ObserverStartListening<AnswerChanel>();
            /*foreach (var item in dependency.ButtonAnsControllers)
            {
                item.IsEnable = true;
            }
            if (MTCCAValueStatic.DragWrongCount == 3)
            {
                StartGuidingDrag(false);
                MTCCAValueStatic.DragWrongCount = 0;
            }
            else
            {
                StartGuidingDrag(true);
            }*/
            StartGuidingDrag(true);
        }

    
        

        public async void StartGuidingDrag(bool isDelay)
        {
            ResetGuidingDrag();
            isGuiding = true;
            SoundChannel soundData;
            try
            {
                foreach (var item in dependency.ButtonAnsControllers)
                {
                    if (item.IsCorrect)
                    {
                        buttonCorrect = item;
                        break;
                    }
                }

                dependency.UiGuiding.transform.localScale = Vector3.one;

                if (isDelay) await UniTask.Delay(dependency.MTCCAGuidingConfig.timeDelayStart, cancellationToken: cts.Token);

                if (tempButtonCorrect == null)
                {
                    tempButtonCorrect = GameObject.Instantiate(buttonCorrect, dependency.TransButtonAnsGroup, false);
                    tempButtonCorrect.name = "TempButtonCorrect";
                    tempButtonCorrect.transform.localPosition = Vector3.zero;
                    tempButtonCorrect.IsEnable = false;
                    tempButtonCorrect.transform.SetAsLastSibling();
                    tempButtonCorrect.CanvasGroup.alpha = 0f;
                }

                bool tscFadeDone = false;
                bool tscMoveDone = false;

                while (isGuiding)
                {
                    dependency.UiGuiding.transform.position = buttonCorrect.transform.position;

                    if (tempButtonCorrect != null) 
                        tempButtonCorrect.transform.position = buttonCorrect.transform.position;

                    dependency.UiGuiding.DOFade(1, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };

                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.MTCCAGuidingConfig.sfxHintAppear);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);

                    if (tempButtonCorrect != null) 
                        tempButtonCorrect.CanvasGroup.DOFade(0.5f, 0.35f).SetEase(Ease.Linear);
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);

                    tscFadeDone = false;
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.MTCCAGuidingConfig.sfxClick);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);

                    SetColorImage(dependency.HandPoint, 0f);
                    SetColorImage(dependency.HandHold, 1f);

                    await UniTask.Delay(dependency.MTCCAGuidingConfig.timeDelay * 3, cancellationToken: cts.Token);

                    if (tempButtonCorrect != null) tempButtonCorrect.transform.DOMove(dependency.TransKhungAns.position, 0.5f).SetEase(Ease.Linear);

                    dependency.UiGuiding.transform.DOMove(dependency.TransKhungAns.position, 0.5f).SetEase(Ease.Linear).onComplete += () => {
                        tscMoveDone = true;
                    };
                    await UniTask.WaitUntil(() => tscMoveDone, cancellationToken: cts.Token);
                    tscMoveDone = false;
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.MTCCAGuidingConfig.sfxUnclick);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    SetColorImage(dependency.HandPoint, 1f);
                    SetColorImage(dependency.HandHold, 0f);
                    await UniTask.Delay(dependency.MTCCAGuidingConfig.timeDelay * 3, cancellationToken: cts.Token);

                    dependency.UiGuiding.DOFade(0, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                    if (tempButtonCorrect != null)
                        tempButtonCorrect.CanvasGroup.DOFade(0, 0.35f).SetEase(Ease.Linear);

                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                    await UniTask.Delay(dependency.MTCCAGuidingConfig.timeDelayStart, cancellationToken: cts.Token);
                }

            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("LinhDo ex: " + ex);
            }
        }
        public void ResetGuidingDrag()
        {
            SoundManager.Instance.StopFx();
            if (tempButtonCorrect != null)
            {

                tempButtonCorrect.CanvasGroup.DOFade(0, 0.1f).onComplete += () =>
                {
                    tempButtonCorrect = null;
                    GameObject.Destroy(tempButtonCorrect);
                    DestroyItem(dependency.TransButtonAnsGroup.transform);
                };
            }
            isGuiding = false;
            dependency.UiGuiding.DOKill();
            dependency.UiGuiding.transform.localScale = Vector3.zero;
            dependency.UiGuiding.DOFade(0f, 0.1f);
        }
        private void DestroyItem(Transform parent)
        {
            if (tempButtonCorrect != null)
            {
                GameObject.Destroy(tempButtonCorrect);
                tempButtonCorrect = null;
            }
               
        }
        private void SetColorImage(Image image, float indexColor)
        {
            Color currentColor = image.color;
            currentColor.a = indexColor;
            image.color = currentColor;
        }
        public override void OnExit()
        {
            base.OnExit();
            ResetGuidingDrag();
            cts?.Cancel();
            this.ObserverStopListening<AnswerChanel>();
        }


        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Dispose();
            //cts?.Cancel();
            this.ObserverStopListening<AnswerChanel>();
        }


    }
    public class MTCCAGuidingStateDependency
    {
        public MTCCAGuidingConfig MTCCAGuidingConfig { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
        public Transform TransButtonAnsGroup { get; set; }
        public Transform TransKhungAns { get; set; }
        public CanvasGroup UiGuiding { get; set; }
        public Image HandPoint { get; set; }
        public Image HandHold { get; set; }
    }
}