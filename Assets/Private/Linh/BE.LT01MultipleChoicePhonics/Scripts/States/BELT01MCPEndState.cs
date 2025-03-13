using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BELT01MCPEndState : FSMState
{
    private BELT01MCPEndStateDependency dependency;
    private CancellationTokenSource cts;
    public override void SetUp(object data)
    {
        dependency = (BELT01MCPEndStateDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        cts = new CancellationTokenSource();
        DoWork();
    }
    private async void DoWork()
    {

    }



}
public class BELT01MCPEndStateDependency
{
    public List<ButtonDemo1> Buttons { get; set; }
    public Button buttonSpeak { get; set; }
}