using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneGuidingState : FSMState, EventListener<AnswerChanel>
    {
        private GameOneGuidingStateDependency dependency;
        private CancellationTokenSource cts;
        private bool isGuiding = false;
        private ButtonAnswerController answerCorrect;
        private const float TIME_DELAY = 500;
        private const float TIME_DELAY_START = 5000;

        public override void SetUp(object data)
        {
            dependency = (GameOneGuidingStateDependency)data;
        }

        public override void OnEnter()
        {
            this.ObserverStartListening<AnswerChanel>();
            DoWork();
        }

        private void DoWork()
        {
            cts = new CancellationTokenSource();
            StartGuidingClick();
        }

        public async void StartGuidingClick()
        {
            ResetGuidingClick();
            isGuiding = true;
            SoundChannel soundData;
            try
            {
                foreach (var item in dependency.ButtonAnswers)
                {
                    if (item.IsCorrect)
                    {
                        answerCorrect = item;
                        break;
                    }
                }
                bool tscFadeDone = false;
                while (isGuiding)
                {
                    dependency.UiGuiding.transform.position = answerCorrect.transform.position;
                    dependency.UiGuiding.transform.localScale = Vector3.one;
                    await UniTask.Delay((int)TIME_DELAY, cancellationToken: cts.Token);

                    dependency.UiGuiding.DOFade(1f, 0.5f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                    //soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, null);
                    //ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                    await UniTask.Delay((int)TIME_DELAY, cancellationToken: cts.Token);

                    for (int i = 0; i < 3; i++)
                    {
                        //soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, null);
                        //ObserverManager.TriggerEvent<SoundChannel>(soundData);
                        SetColorImage(dependency.HandLong, 0f);
                        SetColorImage(dependency.HandShort, 1f);
                        await UniTask.Delay((int)TIME_DELAY * 2, cancellationToken: cts.Token); ;
                        //soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, null);
                        //ObserverManager.TriggerEvent<SoundChannel>(soundData);
                        SetColorImage(dependency.HandLong, 1f);
                        SetColorImage(dependency.HandShort, 0f);
                        await UniTask.Delay((int)TIME_DELAY * 2, cancellationToken: cts.Token);
                    }

                    await UniTask.Delay((int)TIME_DELAY, cancellationToken: cts.Token);
                    tscFadeDone = false;
                    dependency.UiGuiding.DOFade(0f, 0.5f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                    await UniTask.Delay((int)TIME_DELAY_START, cancellationToken: cts.Token);
                }
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("ex: " + ex);
            }
        }

        private void SetColorImage(Image image, float indexColor)
        {
            Color currentColor = image.color;
            currentColor.a = indexColor;
            image.color = currentColor;
        }

        public void ResetGuidingClick()
        {
            SoundManager.Instance.StopFx();
            isGuiding = false;
            answerCorrect = null;
            dependency.UiGuiding.DOKill();
            dependency.UiGuiding.DOFade(0f, 0.1f);
            dependency.UiGuiding.transform.localScale = Vector3.zero;
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
            cts?.Cancel();
            this.ObserverStopListening<AnswerChanel>();
        }

        public void OnMMEvent(AnswerChanel eventType)
        {
            if (eventType.TypeEvent == AnswerChanel.Type.Answer)
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.GuidingFinish, eventType.Data);
                ObserverManager.TriggerEvent(stateChanel);
            }
        }
    }

    public class GameOneGuidingStateDependency
    {
        public List<ButtonAnswerController> ButtonAnswers { get; set; }
        public Image HandLong { get; set; }
        public Image HandShort { get; set; }
        public CanvasGroup UiGuiding { get; set; }
    }
}