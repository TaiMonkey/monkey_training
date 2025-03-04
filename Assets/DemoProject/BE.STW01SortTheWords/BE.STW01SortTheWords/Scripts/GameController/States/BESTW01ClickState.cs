using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01ClickState : FSMState
    {
        private BESTW01ClickStateObjectDependency dependency;
        private CancellationTokenSource cts;
        private int timeDelay = 500;

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            BESTW01ClickStateData clickStateData = (BESTW01ClickStateData)data;
            DoWork(clickStateData);
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01ClickStateObjectDependency)data;
        }

        private async void DoWork(BESTW01ClickStateData clickStateData)
        {
            cts = new CancellationTokenSource();
            //BESTW01HandleData.EnableCards(dependency.CardItems, false);
            dependency.BoxLeft.Enable(false);
            dependency.BoxRight.Enable(false);
            SoundChannel soundData;
            try
            {
                bool tscAudio = false;
                /*if (clickStateData.EventData.UserInput == BESTW01UserInput.ClickCard)
                {
                    BESTW01CardItem currentCard = clickStateData.EventData.ObjectEvent.GetComponent<BESTW01CardItem>();
                    await UniTask.Delay(timeDelay, cancellationToken: cts.Token);
                    currentCard.IsPlayAudio = true;
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, currentCard.DataCard.audio, () => { 
                        tscAudio = true;
                        currentCard.IsPlayAudio = false;
                    });
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    await UniTask.WaitUntil(() => tscAudio, cancellationToken: cts.Token);
                }
                else*/ if(clickStateData.EventData.UserInput == BESTW01UserInput.ClickBox)
                {
                    dependency.Guiding.ResetGuiding();
                    bool tscAnimDone = false;
                    BESTW01Box currentBox = clickStateData.EventData.ObjectEvent.GetComponent<BESTW01Box>();
                    int turnBox = (currentBox.GetData().typeBox == BESTW01TypeBox.Left) ? BESTW01HandleData.CurrentTurnBoxLeft : BESTW01HandleData.CurrentTurnBoxRight;
                    int numberBox = (int)currentBox.GetData().typeBox;
                    if (turnBox == BESTW01HandleData.MAX_TURN_BOX)
                    {
                        BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxGreenTap, numberBox, false, (trackEntry) => {
                        BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxGreen, numberBox, false, null);
                        tscAnimDone = true;
                        });
                    }
                    else
                    {
                        BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxTap,numberBox, turnBox, false, (trackEntry) => {
                            BESTW01HandleData.SetAnimation(currentBox.GetSkeleton(), dependency.BoxConfig.boxNormal, numberBox, turnBox, false, null);
                            tscAnimDone = true;
                        });
                    }                 
                  
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.BoxConfig.sfxSelect);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    await UniTask.Delay(timeDelay, cancellationToken: cts.Token);
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, currentBox.GetData().audio, () => { tscAudio = true; });
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    await UniTask.WaitUntil(() => tscAudio && tscAnimDone, cancellationToken: cts.Token);
                }else
                {
                    dependency.Guiding.ResetGuiding();
                }

                BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("Lucanhtai ex: " + ex);
            }
        }


        public override void OnExit()
        {
            base.OnExit();
            cts?.Cancel();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Cancel();
            cts?.Dispose();
        }
    }
    public class BESTW01ClickStateData
    {
        public BESTW01ClickStateEventData EventData { get; set; }
    }

    public class BESTW01ClickStateEventData
    {
        public GameObject ObjectEvent { get; set; }
        public BESTW01UserInput UserInput { get; set; }
    }

    public class BESTW01ClickStateObjectDependency
    {
        public BESTW01BoxConfig BoxConfig { get; set; }
        public BESTW01Guiding Guiding { get; set; }
        public List<BESTW01CardItem> CardItems { get; set; }
        public BESTW01Box BoxLeft { get; set; }
        public BESTW01Box BoxRight { get; set; }
    }
}