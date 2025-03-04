using Cysharp.Threading.Tasks;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01PlayState : FSMState
    {
        private BESTW01PlayStateObjectDependency dependency;
        private CancellationTokenSource cts;
        private bool tscBoxLeftPlaying = false;
        private bool tscBoxRightPlaying = false;

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            tscBoxLeftPlaying = dependency.BoxLeft.IsPlaying;
            tscBoxRightPlaying = dependency.BoxRight.IsPlaying;
            DoWork();
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01PlayStateObjectDependency)data;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            tscBoxLeftPlaying = dependency.BoxLeft.IsPlaying;
            tscBoxRightPlaying = dependency.BoxRight.IsPlaying;
            if ((BESTW01HandleData.CurrentTurnBoxLeft + BESTW01HandleData.CurrentTurnBoxRight) >= BESTW01HandleData.MAX_TURN_DRAG)
            {
                CheckEndGameUpdate();
            }
        }

        private async void CheckEndGameUpdate()
        {
            try
            {
                await UniTask.WaitUntil(() => !tscBoxLeftPlaying && !tscBoxRightPlaying, cancellationToken: cts.Token);
                BESTW01HandleData.TriggerFinishState(BESTW01State.EndGame, null);
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("Lucanhtai ex: " + ex);
            }
            return;
        }

        private async void DoWork()
        {
            cts = new CancellationTokenSource();
            try
            {
                if ((BESTW01HandleData.CurrentTurnBoxLeft + BESTW01HandleData.CurrentTurnBoxRight) >= BESTW01HandleData.MAX_TURN_DRAG)
                {
                    await UniTask.WaitUntil(() => !tscBoxLeftPlaying && !tscBoxRightPlaying, cancellationToken: cts.Token);
                    BESTW01HandleData.TriggerFinishState(BESTW01State.EndGame, null);
                }else
                {
                    BESTW01HandleData.EnableCards(dependency.CardItems, true);
                    dependency.BoxLeft.Enable(true);
                    dependency.BoxRight.Enable(true);

                    List<BESTW01CardItem> listCardSelected = new();
                    if (listCardSelected.Count > 0) listCardSelected.Clear();

                    foreach (var item in dependency.CardItems)
                    {
                        if (!item.IsDragged) listCardSelected.Add(item);
                    }
                    dependency.Guiding.InitData(listCardSelected);
                    dependency.Guiding.StartGuiding(true);
                }
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

    public class BESTW01PlayStateObjectDependency
    {
        public BESTW01BoxConfig BoxConfig { get; set; }
        public BESTW01Guiding Guiding { get; set; }
        public List<BESTW01CardItem> CardItems { get; set; }
        public BESTW01Box BoxLeft { get; set; }
        public BESTW01Box BoxRight { get; set; }
    }
}