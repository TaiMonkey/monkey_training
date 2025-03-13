using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BELT01MCPDragResult : FSMState
{
    private BELT01MCPDragResultObjectDependency dependency;
    public CancellationTokenSource cts;

    public override void SetUp(object data)
    {
        dependency = (BELT01MCPDragResultObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        ButtonDemo1 buttonDemo1 = (ButtonDemo1)data;
        DoWork(buttonDemo1);
    }
    private async void DoWork(ButtonDemo1 buttonDemo1)
    {
        cts = new CancellationTokenSource();
        try
        {
            if (buttonDemo1.Databutton.iscorrect)
            {
                BELT01ValueStatic.DragCorrectCount++;
                buttonDemo1.transform.DOMove(dependency.TransSpeaker.position, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                {
                    buttonDemo1.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete += () =>
                    {
                        BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.DragResualEnd, null);
                        ObserverManager.TriggerEvent(bELT01MCPDataChanner);
                    };
                };
            }
            else
            {
                BELT01ValueStatic.DragCorrectCount++;
                buttonDemo1.OnBackButton(() =>
                {
                    buttonDemo1.IsEnable = true;
                    BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.DragResualEnd, null);
                    ObserverManager.TriggerEvent(bELT01MCPDataChanner);

                });
            }
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
        cts?.Cancel();
        cts?.Dispose();

    }
}
public class BELT01MCPDragResultObjectDependency 
{
    public Transform TransSpeaker { get; set; }
}
