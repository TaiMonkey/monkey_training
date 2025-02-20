using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System;

public class DemoFSMStateDrag : FSMState
{
    private DemoFSMStateDragDependency dependency;
    public CancellationTokenSource cts;
    public override void SetUp(object data)
    {
        dependency = (DemoFSMStateDragDependency)data;
    }

    public override void OnEnter(object data)
    {
        ButtonAnswer buttonAnswer = (ButtonAnswer)data;
        DoWork(buttonAnswer);
        Debug.LogError(buttonAnswer.IsEnable);
    }

    private async void DoWork(ButtonAnswer buttonAnswer)
    {
        cts = new CancellationTokenSource();
        try
        {
            if(buttonAnswer.isCorrect)
            {
                buttonAnswer.transform.DOMove(dependency.buttonSpeaker.position, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                {
                    buttonAnswer.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                    {
                        // event DragEnd
                    };
                };
            }
            else
            {
                buttonAnswer.OnBackButton(() =>
                {
                    buttonAnswer.IsEnable = true;
                });
            }

        }catch(OperationCanceledException e)
        {
            Debug.LogError(e);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        cts.Cancel();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        cts?.Dispose();
        cts?.Cancel();
    }
}


public class DemoFSMStateDragDependency
{
    public Transform buttonSpeaker { get; set; }
}
