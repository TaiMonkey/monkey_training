using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01DragResultState : FSMState
    {
        private BESTW01DragResultStateObjectDependency dependency;
        private CancellationTokenSource cts;
        private bool isPopUp = false;
        private float lastProgress = 0f;
        private BESTW01CardItem currentCard;
        private BESTW01Box currentBox;

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
           /* BESTW01DragResultStateData dragResultStateData = (BESTW01DragResultStateData)data;
            DoWork(dragResultStateData);*/
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01DragResultStateObjectDependency)data;
        }

       /* private async void DoWork(BESTW01DragResultStateData resultStateData)
        {
            cts = new CancellationTokenSource();
            BESTW01HandleData.EnableCards(dependency.CardItems, false);
            dependency.BoxLeft.Enable(false);
            dependency.BoxRight.Enable(false);
            SoundChannel soundData;
            try
            {
                currentCard = resultStateData.EventData.CardObject.GetComponent<BESTW01CardItem>();
                SkeletonGraphic cardAnimation = currentCard.GetSkeleton();
                currentBox = resultStateData.EventData.BoxObject.GetComponent<BESTW01Box>();
                int turnBox = (currentBox.GetData().typeBox == BESTW01TypeBox.Left) ? resultStateData.CurrentTurnBoxLeft : resultStateData.CurrentTurnBoxRight;

                bool tscMoveDone = false;
                bool tscAnimationDone = false;
                bool tscAudio = false;
                bool isCorrect = currentCard.GetData().typeBox == currentBox.GetData().typeBox;

                if (isCorrect) {

                    currentCard.transform.DOKill();
                    currentCard.SetDragged(true);
                    isPopUp = true;
                    BESTW01HandleData.SetAnimation(cardAnimation, dependency.CardConfig.cardFlyToBox, false, (trackEntry) => { 
                        tscAnimationDone = true;
                        isPopUp = false;
                    });
                    currentCard.transform.DOMove(currentBox.transform.position, 0.35f).SetEase(Ease.InOutSine).OnComplete(() => { tscMoveDone = true; });
                    await UniTask.WaitUntil(() => tscMoveDone && tscAnimationDone, cancellationToken: cts.Token);
                    tscAnimationDone = false;
                   
                    currentCard.transform.localScale = Vector3.zero;
                    BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxPopup, (int)currentBox.GetData().typeBox, turnBox, false, (trackEntry) => {
                        tscAnimationDone = true;
                    });
                    await UniTask.WaitUntil(() => tscAnimationDone, cancellationToken: cts.Token);
                    await UniTask.Delay(dependency.DragResultConfig.timeDelay, cancellationToken: cts.Token);
                    tscAnimationDone = false;

                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DragResultConfig.sfxCorrect);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxCorrect, (int)currentBox.GetData().typeBox, turnBox, false, (trackEntry) => {
                        BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxNormal, (int)currentBox.GetData().typeBox, turnBox + 1, false, null);
                        tscAnimationDone = true;
                    });
                    bool tscFireDone = false;
                    int randomIndex = UnityEngine.Random.RandomRange(0, dependency.BoxConfig.fireworks.Length);
                    SkeletonGraphic boxFirework = currentBox.GetFirework();
                    boxFirework.gameObject.SetActive(true);

                    BESTW01HandleData.SetAnimation(boxFirework, dependency.BoxConfig.fireworks[randomIndex], false, (trackEntry) => {
                        boxFirework.gameObject.SetActive(false);
                        tscFireDone = true;
                    });
                    await UniTask.WaitUntil(() => tscAnimationDone && tscFireDone, cancellationToken: cts.Token);
                    BESTW01HandleData.TriggerFinishState(BESTW01State.DragCorrect, currentBox.GetData().typeBox);
                }
                else
                {
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DragResultConfig.sfxWrong);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    BESTW01HandleData.TriggerStateCarousel(BESTW01UserInput.UnDragCardCarousel, (currentCard.gameObject, false));

                    BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxWrong,
                   (int)currentBox.GetData().typeBox, turnBox, false, (trackEntry) => {
                       BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxNormal, (int)currentBox.GetData().typeBox, turnBox, false, null);
                       tscAnimationDone = true;
                   });
                    await UniTask.WaitUntil(() => tscAnimationDone, cancellationToken: cts.Token);
                    BESTW01HandleData.TriggerFinishState(BESTW01State.DragWrong, null);

                }
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("Lucanhtai ex: " + ex);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (isPopUp)
            {
                var trackEntry = currentCard.GetSkeleton().AnimationState.GetCurrent(0);
                if (trackEntry != null)
                {
                    float currentTime = trackEntry.AnimationTime;
                    float totalDuration = trackEntry.Animation.Duration;

                    float progress = currentTime / totalDuration;

                    CheckProgress(progress);
                }
            }

        }

        private void CheckProgress(float progress)
        {
            if (progress >= 0.5f && lastProgress < 0.5f)
            {
                lastProgress = 0.5f;
                currentCard.transform.SetParent(currentBox.transform);
                currentCard.transform.SetAsFirstSibling();     
            }
        }*/

        public override void OnExit()
        {
            base.OnExit();
            cts?.Cancel();
            isPopUp = false;
            lastProgress = 0f;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Cancel();
            cts?.Dispose();
        }
    }

    public class BESTW01DragResultStateData
    {
        public int CurrentTurnBoxLeft { get; set; }
        public int CurrentTurnBoxRight { get; set; }
        public BESTW01DragResultStateEventData EventData { get; set; }
    }

    public class BESTW01DragResultStateEventData
    {
        public GameObject CardObject { get; set; }
        public GameObject BoxObject { get; set; }
    }

    public class BESTW01DragResultStateObjectDependency
    {
        public BESTW01SDragResultConfig DragResultConfig { get; set; }
        public BESTW01BoxConfig BoxConfig { get; set; }
        public BESTW01CardConfig CardConfig { get; set; }
        public List<BESTW01CardItem> CardItems { get; set; }
        public BESTW01Box BoxLeft { get; set; }
        public BESTW01Box BoxRight { get; set; }
    }
}