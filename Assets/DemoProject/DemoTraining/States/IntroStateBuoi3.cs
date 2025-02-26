using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IntroStateBuoi3 : FSMState
{
    private IntroStateBuoi3ObjectDependency dependency;

    public override void SetUp(object data)
    {
        dependency = (IntroStateBuoi3ObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        string dataSendInit = (string)data;
        Debug.LogError("Intro playing : " + dataSendInit);
        DoWork();
    }

    private CancellationTokenSource cts;
    private async void DoWork()
    {
        cts = new CancellationTokenSource();
        try
        {
            bool isPlaySound = false;
            SoundChannel soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT,
                dependency.IntroConfig.audioCta, () => {
                    isPlaySound = true;
                });
            ObserverManager.TriggerEvent<SoundChannel>(soundData);
            await UniTask.WaitUntil(() => isPlaySound, cancellationToken: cts.Token);
            await UniTask.Delay(dependency.IntroConfig.timeDelay, cancellationToken: cts.Token);

            bool isPopupFish = false;
            for(int i = 0; i< dependency.ListFish.Count; i++)
            {
                DemoFishButton fishButton = dependency.ListFish[i];
                fishButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).onComplete += () => {
                    isPopupFish = true;
                };
                await UniTask.WaitUntil(() => isPopupFish, cancellationToken: cts.Token);
                isPopupFish = false;
            }
            await UniTask.Delay(dependency.IntroConfig.timeDelay, cancellationToken: cts.Token);
            for (int i = 0; i < dependency.ListFish.Count; i++)
            {
                DemoFishButton fishButton = dependency.ListFish[i];
                fishButton.OriginPos = fishButton.transform.position;
                fishButton.IsEnable = true;
            }

            Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.IntroEnd, "ddd");
            ObserverManager.TriggerEvent(buoi3Channer);
        }
        catch (OperationCanceledException e)
        {
            Debug.Log(e);
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
        cts?.Dispose();
        cts?.Cancel();
    }

}

public class IntroStateBuoi3ObjectDependency
{
    public DemoIntroConfig IntroConfig { get; set; }
    public List<DemoFishButton> ListFish { get; set; }
         
}
