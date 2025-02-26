using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GuidingStateBuoi3 : FSMState
{
    private GuidingStateBuoi3ObjectDependency dependency;
    private CancellationTokenSource cts;
    private bool isGuiding = false;
    private DemoFishButton demoFishButtonCorrect;
    private DemoFishButton tempFishButtonCorrect;

    public override void SetUp(object data)
    {
        dependency = (GuidingStateBuoi3ObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        
        DoWork();
    }

    private void DoWork()
    {
        cts = new CancellationTokenSource();
        Debug.LogError("DragWrongCount: "  + Buoi3ValueStatic.DragWrongCount);
        Debug.LogError("DragCorrectCount: " + Buoi3ValueStatic.DragCorrectCount);
        if(Buoi3ValueStatic.DragCorrectCount == 1)
        {
            Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.NextTurnStart, null);
            ObserverManager.TriggerEvent(buoi3Channer);
        }
        else
        {
            foreach(var item in dependency.ListFish)
            {
                item.IsEnable = true;
            }
            if (Buoi3ValueStatic.DragWrongCount == 3)
            {
                StartGuidingDrag(false);
                Buoi3ValueStatic.DragWrongCount = 0;
            }
            else
            {
                StartGuidingDrag(true);
            }
        }
    }

    //Guiding click
    public async void StartGuidingClick(bool isDelay)
    {
        ResetGuidingClick();
        isGuiding = true;
        SoundChannel soundData;
        try
        {
            foreach(var item in dependency.ListFish)
            {
                if(item.isCorrect)
                {
                    demoFishButtonCorrect = item;
                    break;
                }
            }
            if (isDelay) await UniTask.Delay(dependency.DemoGuidingConfig.timeDelayStart, cancellationToken: cts.Token);
            bool tscFadeDone = false;
            while (isGuiding)
            {
                dependency.UiGuiding.transform.localScale = Vector3.one;
                dependency.UiGuiding.transform.position = demoFishButtonCorrect.transform.position;
                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay, cancellationToken: cts.Token);

                dependency.UiGuiding.DOFade(1f, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DemoGuidingConfig.sfxAppear);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay, cancellationToken: cts.Token);

                for (int i = 0; i < 3; i++)
                {
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DemoGuidingConfig.sfxClick);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    SetColorImage(dependency.HandLong, 0f);
                    SetColorImage(dependency.HandShort, 1f);
                    await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay * 2, cancellationToken: cts.Token); ;
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DemoGuidingConfig.sfxUnClick);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    SetColorImage(dependency.HandLong, 1f);
                    SetColorImage(dependency.HandShort, 0f);
                    await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay * 2, cancellationToken: cts.Token);
                }

                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay, cancellationToken: cts.Token);
                tscFadeDone = false;
                dependency.UiGuiding.DOFade(0f, 0.5f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelayStart, cancellationToken: cts.Token);
            }
        }
        catch (OperationCanceledException ex)
        {
            LogMe.Log("Lucanhtai ex: " + ex);
        }
    }
    public void ResetGuidingClick()
    {
        SoundManager.Instance.StopFx();
        isGuiding = false;
        demoFishButtonCorrect = null;
        dependency.UiGuiding.DOKill();
        dependency.UiGuiding.DOFade(0f, 0.1f);
        dependency.UiGuiding.transform.localScale = Vector3.zero;
    }

    //Guiding drag
    public async void StartGuidingDrag(bool isDelay)
    {
        ResetGuidingDrag();
        isGuiding = true;
        SoundChannel soundData;
        try
        {
            foreach (var item in dependency.ListFish)
            {
                if (item.isCorrect)
                {
                    demoFishButtonCorrect = item;
                    break;
                }
            }
            dependency.UiGuiding.transform.localScale = Vector3.one;
            if (isDelay) await UniTask.Delay(dependency.DemoGuidingConfig.timeDelayStart, cancellationToken: cts.Token);
            if (tempFishButtonCorrect == null)
            {
                tempFishButtonCorrect = GameObject.Instantiate(demoFishButtonCorrect, dependency.ButtonGroup, false);
                tempFishButtonCorrect.name = "TempFishCorrect";
                tempFishButtonCorrect.transform.localPosition = Vector3.zero;
                tempFishButtonCorrect.IsEnable = false;
                tempFishButtonCorrect.transform.SetAsLastSibling();
                tempFishButtonCorrect.CanvasGroup.alpha = 0f;
            }
            bool tscFadeDone = false;
            bool tscMoveDone = false;
            while (isGuiding)
            {
                dependency.UiGuiding.transform.position = demoFishButtonCorrect.transform.position;
                if (tempFishButtonCorrect != null) tempFishButtonCorrect.transform.position
                        = demoFishButtonCorrect.transform.position;

                dependency.UiGuiding.DOFade(1, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DemoGuidingConfig.sfxAppear);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                if (tempFishButtonCorrect != null) tempFishButtonCorrect.CanvasGroup.DOFade(0.5f, 0.35f).SetEase(Ease.Linear);
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                tscFadeDone = false;
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DemoGuidingConfig.sfxClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                SetColorImage(dependency.HandLong, 0f);
                SetColorImage(dependency.HandShort, 1f);
                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay * 3, cancellationToken: cts.Token);

                if (tempFishButtonCorrect != null) tempFishButtonCorrect.transform.DOMove(dependency.TransImage.position, 0.5f).SetEase(Ease.Linear);
               
                dependency.UiGuiding.transform.DOMove(dependency.TransImage.position, 0.5f).SetEase(Ease.Linear).onComplete += () => {
                    tscMoveDone = true;
                };
                await UniTask.WaitUntil(() => tscMoveDone, cancellationToken: cts.Token);
                tscMoveDone = false;
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.DemoGuidingConfig.sfxUnClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                SetColorImage(dependency.HandLong, 1f);
                SetColorImage(dependency.HandShort, 0f);
                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelay * 3, cancellationToken: cts.Token);

                dependency.UiGuiding.DOFade(0, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                if (tempFishButtonCorrect != null) tempFishButtonCorrect.CanvasGroup.DOFade(0, 0.35f).SetEase(Ease.Linear);
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.DemoGuidingConfig.timeDelayStart, cancellationToken: cts.Token);
            }

        }
        catch (OperationCanceledException ex)
        {
            LogMe.Log("Lucanhtai ex: " + ex);
        }
    }

    public void ResetGuidingDrag()
    {
        SoundManager.Instance.StopFx();
        if (tempFishButtonCorrect != null)
        {

            tempFishButtonCorrect.CanvasGroup.DOFade(0, 0.1f).onComplete += () =>
            {
                tempFishButtonCorrect = null;
                GameObject.Destroy(tempFishButtonCorrect);
                DestroyItem(dependency.ButtonGroup.transform);
            };
        }
        isGuiding = false;
        dependency.UiGuiding.DOKill();
        dependency.UiGuiding.transform.localScale = Vector3.zero;
        dependency.UiGuiding.DOFade(0f, 0.1f);
    }

    private void DestroyItem(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name.Equals("TempFishCorrect")) GameObject.Destroy(child.gameObject);
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
        //ResetGuidingClick();
        ResetGuidingDrag();
        cts?.Cancel();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        cts?.Dispose();
        cts?.Cancel();
    }

}

public class GuidingStateBuoi3ObjectDependency
{
    public DemoGuidingConfig DemoGuidingConfig { get; set; }
    public List<DemoFishButton> ListFish { get; set; }
    public Transform TransImage { get; set; }
    public Transform ButtonGroup { get; set; }
    public CanvasGroup UiGuiding { get; set; }
    public Image HandLong { get; set; }
    public Image HandShort { get; set; }
}

