using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BELT01MCPGuidingState : FSMState
{
    private BELT01MCPGuidingStateDependency dependency;
    private CancellationTokenSource cts;
    private bool isGuiding = false;
    private ButtonDemo1 buttonCorrect;
    private ButtonDemo1 tempButtonCorrect;

    public override void SetUp(object data)
    {
        dependency = (BELT01MCPGuidingStateDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);

        DoWork();
    }

    private void DoWork()
    {
        cts = new CancellationTokenSource();
        Debug.LogError("DragWrongCount: " + BELT01ValueStatic.DragWrongCount);
        Debug.LogError("DragCorrectCount: " + BELT01ValueStatic.DragCorrectCount);
        if (BELT01ValueStatic.DragCorrectCount == 1)
        {
            BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.NextTurnStart, null);
            ObserverManager.TriggerEvent(bELT01MCPDataChanner);
        }
        else
        {
            foreach (var item in dependency.ListButton)
            {
                item.IsEnable = true;
            }
            if (BELT01ValueStatic.DragWrongCount == 3)
            {
                StartGuidingDrag(false);
                BELT01ValueStatic.DragWrongCount = 0;
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
            foreach (var item in dependency.ListButton)
            {
                if (item.Databutton.iscorrect)
                {
                    buttonCorrect = item;
                    break;
                }
            }
            if (isDelay) await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelayStart, cancellationToken: cts.Token);
            bool tscFadeDone = false;
            while (isGuiding)
            {
                dependency.UiGuiding.transform.localScale = Vector3.one;
                dependency.UiGuiding.transform.position = buttonCorrect.transform.position;
                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay, cancellationToken: cts.Token);

                dependency.UiGuiding.DOFade(1f, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.bELT01GuidingConfig.sfxAppear);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay, cancellationToken: cts.Token);

                for (int i = 0; i < 3; i++)
                {
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.bELT01GuidingConfig.sfxClick);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    SetColorImage(dependency.HandLong, 0f);
                    SetColorImage(dependency.HandShort, 1f);
                    await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay * 2, cancellationToken: cts.Token); ;
                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.bELT01GuidingConfig.sfxUnClick);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);
                    SetColorImage(dependency.HandLong, 1f);
                    SetColorImage(dependency.HandShort, 0f);
                    await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay * 2, cancellationToken: cts.Token);
                }

                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay, cancellationToken: cts.Token);
                tscFadeDone = false;
                dependency.UiGuiding.DOFade(0f, 0.5f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelayStart, cancellationToken: cts.Token);
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
        buttonCorrect = null;
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
            foreach (var item in dependency.ListButton)
            {
                if (item.Databutton.iscorrect)
                {
                    buttonCorrect = item;
                    break;
                }
            }
            dependency.UiGuiding.transform.localScale = Vector3.one;
            if (isDelay) await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelayStart, cancellationToken: cts.Token);
            if (tempButtonCorrect == null)
            {
                tempButtonCorrect = GameObject.Instantiate(buttonCorrect, dependency.ButtonGroup, false);
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
                if (tempButtonCorrect != null) tempButtonCorrect.transform.position
                        = buttonCorrect.transform.position;

                dependency.UiGuiding.DOFade(1, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.bELT01GuidingConfig.sfxAppear);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                if (tempButtonCorrect != null) tempButtonCorrect.CanvasGroup.DOFade(0.5f, 0.35f).SetEase(Ease.Linear);
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                tscFadeDone = false;
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.bELT01GuidingConfig.sfxClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                SetColorImage(dependency.HandLong, 0f);
                SetColorImage(dependency.HandShort, 1f);
                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay * 3, cancellationToken: cts.Token);

                if (tempButtonCorrect != null) tempButtonCorrect.transform.DOMove(dependency.TransSpeak.position, 0.5f).SetEase(Ease.Linear);

                dependency.UiGuiding.transform.DOMove(dependency.TransSpeak.position, 0.5f).SetEase(Ease.Linear).onComplete += () => {
                    tscMoveDone = true;
                };
                await UniTask.WaitUntil(() => tscMoveDone, cancellationToken: cts.Token);
                tscMoveDone = false;
                soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.bELT01GuidingConfig.sfxUnClick);
                ObserverManager.TriggerEvent<SoundChannel>(soundData);
                SetColorImage(dependency.HandLong, 1f);
                SetColorImage(dependency.HandShort, 0f);
                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelay * 3, cancellationToken: cts.Token);

                dependency.UiGuiding.DOFade(0, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                if (tempButtonCorrect != null) tempButtonCorrect.CanvasGroup.DOFade(0, 0.35f).SetEase(Ease.Linear);
                await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                await UniTask.Delay(dependency.bELT01GuidingConfig.timeDelayStart, cancellationToken: cts.Token);
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

public class BELT01MCPGuidingStateDependency
{

    public BELT01GuidingConfig bELT01GuidingConfig { get; set; }
    public List<ButtonDemo1> ListButton { get; set; }
    public Transform TransSpeak { get; set; }
    public Transform ButtonGroup { get; set; }
    public CanvasGroup UiGuiding { get; set; }
    public Image HandLong { get; set; }
    public Image HandShort { get; set; }
}
