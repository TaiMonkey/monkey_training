using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BELT01MCPIntroState : FSMState
{
    private BELT01MCPIntroStateObjectDependency dependency;
    public CancellationTokenSource cts;

    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        string dataSendInit = (string)data;
        Debug.LogError("Intro playing: " + dataSendInit);
        DOWork();
    }
    private async void DOWork()
    {
        cts = new CancellationTokenSource();
        try
        {
            bool isPlaySound = false;
            int index = UnityEngine.Random.RandomRange(0, dependency.IntroConfig.sfxCTA.Count);
            SoundChannel sounData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.IntroConfig.sfxCTA[index], () =>
            {
                isPlaySound = true;
            });
            ObserverManager.TriggerEvent<SoundChannel>(sounData);
            await UniTask.WaitUntil(() => isPlaySound, cancellationToken: cts.Token);
            await UniTask.Delay(dependency.IntroConfig.timeDelay, cancellationToken: cts.Token);

            bool isScaleButton = false;
            for(int i =0; i < dependency.listButtonans.Count; i++)
            {
                ButtonDemo1 buttonDemo1 = dependency.listButtonans[i];
                buttonDemo1.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).onComplete += () =>
                {
                    isScaleButton = true;
                };
                await UniTask.WaitUntil(() => isPlaySound, cancellationToken: cts.Token);
                isScaleButton = false;
            }
            await UniTask.Delay(dependency.IntroConfig.timeDelay, cancellationToken: cts.Token);
            for(int i= 0; i< dependency.listButtonans.Count; i++)
            {
                ButtonDemo1 buttonDemo1 = dependency.listButtonans[i];
                buttonDemo1.OriginPos = buttonDemo1.transform.position;
                buttonDemo1.IsEnable = true;
            }
            BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.IntroStateEnd, "dhudhau");
            ObserverManager.TriggerEvent(bELT01MCPDataChanner);

        }
        catch(OperationCanceledException e)
        {
            Debug.Log(e);
        }
    }
    public override void SetUp(object data)
    {
        dependency = (BELT01MCPIntroStateObjectDependency)data;
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
public class BELT01MCPIntroStateObjectDependency
{
    public BELT01MCPIntroConfig IntroConfig { get; set; }
    public List<ButtonDemo1> listButtonans { get; set; }

}
