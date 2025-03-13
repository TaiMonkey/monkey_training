using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DragResultBuoi3 : FSMState
{
    private DragResultBuoi3ObjectDependency dependency;
    private CancellationTokenSource cts;
    public override void SetUp(object data)
    {
        dependency = (DragResultBuoi3ObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        DemoFishButton fishDrag = (DemoFishButton)data;
        DoWork(fishDrag);
    }

    private async void DoWork(DemoFishButton fishDrag)
    {
        cts = new CancellationTokenSource();
        try
        {
            if (fishDrag.isCorrect)
            {
                Buoi3ValueStatic.DragCorrectCount++;
                fishDrag.transform.DOMove(dependency.TransImage.position, 0.3f).SetEase(Ease.Linear).onComplete +=
                    () => {
                        fishDrag.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).onComplete +=
                        () => {
                            Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.DragResultEnd, null);
                            ObserverManager.TriggerEvent(buoi3Channer);
                        };

                    };
            }
            else
            {
                Buoi3ValueStatic.DragWrongCount++;
                fishDrag.OnBackButton(() =>
                {
                    fishDrag.IsEnable = true;
                    Buoi3Channer buoi3Channer = new Buoi3Channer(Buoi3StatusOfStateState.DragResultEnd, null);
                    ObserverManager.TriggerEvent(buoi3Channer);
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
        cts?.Dispose();
        cts?.Cancel();
    }

}

public class DragResultBuoi3ObjectDependency
{
    public Transform TransImage { get; set; }
}

