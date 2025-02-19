using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;

public class DemoFSMStateIntro : FSMState
{
    public DemoStateIntroDependency dependency;
    public CancellationTokenSource cts;
    public override void SetUp(object data)
    {
        dependency = (DemoStateIntroDependency)data;
    }

    public override void OnEnter(object data)
    {
        //DemoStateIntroDependency demoGamePlayData = (DemoStateIntroDependency)data;
        Debug.LogError("Intro start");
        DoWork();
    }

    private async void DoWork()
    {
        cts = new CancellationTokenSource();
        ButtonSpeaker buttonSpeaker = dependency.buttonSpeaker;
        buttonSpeaker.canvasGroup.DOFade(1, 0.2f);
        await UniTask.Delay(200, cancellationToken : cts.Token);
        bool isPopup = false;
        bool isPlaySoud = false;
        for(int i = 0; i < dependency.listAnswer.Count; i++)
        {
            ButtonAnswer buttonAnswer = dependency.listAnswer[i];
            buttonAnswer.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear).onComplete += () =>
            {
                isPopup = true;
            };
            SoundChannel soudData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.listAudioPopup[i], () =>
            {
                isPlaySoud = true;
            });
            ObserverManager.TriggerEvent(soudData);
            await UniTask.Delay(200, cancellationToken: cts.Token);
            isPopup = false;
            isPlaySoud = false;
        }
        SoundChannel soudDataFinal = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.audioCta, () =>
        {
            isPlaySoud = true;
        });
        ObserverManager.TriggerEvent(soudDataFinal);
        // buttonSpeaker.transform.DOF
    }
}


public class DemoStateIntroDependency
{
    public AudioClip audioCta;
    public ButtonSpeaker buttonSpeaker;
    public List<ButtonAnswer> listAnswer;
    public List<AudioClip> listAudioPopup;
}
