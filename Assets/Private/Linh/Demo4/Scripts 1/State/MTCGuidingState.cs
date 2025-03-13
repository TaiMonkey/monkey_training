using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTC
{
    public class MTCGuidingState : FSMState, EventListener<AnswerChanel>
    {
        private MTCGuidingDependency dependency;
        private CancellationTokenSource cts;
        private ButtonAnsController buttonCorrect;
        private bool isGuiding = false;
        public override void SetUp(object data)
        {
            dependency = (MTCGuidingDependency)data;
        }
        public override void OnEnter(object data)
        {
            base.OnEnter();
            cts = new CancellationTokenSource();
            StartGuidingClick(true);
            this.ObserverStartListening<AnswerChanel>();

        }
        public async void StartGuidingClick(bool isDelay)
        {
            ResetGuidingClick();
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

                if (isDelay) await UniTask.Delay(dependency.MTCGuidingConfig.timeDelayStart, cancellationToken: cts.Token);
                bool tscFadeDone = false;
                while (isGuiding)
                {
                    dependency.UiGuiding.transform.localScale = Vector3.one;
                    dependency.UiGuiding.transform.position = buttonCorrect.transform.position;
                    await UniTask.Delay(dependency.MTCGuidingConfig.timeDelay, cancellationToken: cts.Token);

                    dependency.UiGuiding.DOFade(1f, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.MTCGuidingConfig.sfxAppear);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                    await UniTask.Delay(dependency.MTCGuidingConfig.timeDelay, cancellationToken: cts.Token);

                    for (int i = 0; i < 3; i++)
                    {
                        soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.MTCGuidingConfig.sfxClick);
                        ObserverManager.TriggerEvent<SoundChannel>(soundData);
                        SetColorImage(dependency.HandPoint, 0f);
                        SetColorImage(dependency.HandHold, 1f);
                        await UniTask.Delay(dependency.MTCGuidingConfig.timeDelay * 2, cancellationToken: cts.Token); ;
                        soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.MTCGuidingConfig.sfxUnClick);
                        ObserverManager.TriggerEvent<SoundChannel>(soundData);
                        SetColorImage(dependency.HandPoint, 1f);
                        SetColorImage(dependency.HandHold, 0f);
                        await UniTask.Delay(dependency.MTCGuidingConfig.timeDelay * 2, cancellationToken: cts.Token);

                    }
                    await UniTask.Delay(dependency.MTCGuidingConfig.timeDelay, cancellationToken: cts.Token);
                    tscFadeDone = false;
                    dependency.UiGuiding.DOFade(0f, 0.5f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                    await UniTask.Delay(dependency.MTCGuidingConfig.timeDelayStart, cancellationToken: cts.Token);

                }
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("Linh ex: " + ex);
            }
        }

        public void ResetGuidingClick()
        {
            SoundManager.Instance.StopFx();
            isGuiding = false;
            buttonCorrect = null;
            dependency.UiGuiding.DOKill();
            dependency.UiGuiding.DOFade(0f, 0.1f);
            dependency.UiGuiding.transform.localScale = Vector3.zero;
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
            ResetGuidingClick();
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
        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.GuiddingFinish, eventType.Data);
                ObserverManager.TriggerEvent(stateChanel);
            }
        }
    }
    public class MTCGuidingDependency
    {
        public MTCGuidingConfig MTCGuidingConfig { get; set; }
        public List<ButtonAnsController> ButtonAnsControllers { get; set; }
        public Transform TransButtonAnsGroup { get; set; }
        public CanvasGroup UiGuiding { get; set; }
        public Image HandPoint { get; set; }
        public Image HandHold { get; set; }
    }
}
