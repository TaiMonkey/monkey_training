using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameManager : GameManager, EventListener<DemoChannel>
{
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ObserverStartListening<DemoChannel>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.ObserverStopListening<DemoChannel>();
    }


    protected override void Start()
    {
        base.Start();
        string data = "";
        fSMSystem.SetupStateData(dependency);
        adapter.SetData(data);
        fSMSystem.GotoState(DemoConstValue.Init.ToString(), adapter.GetData<DemoGamePlayData1>(0));
    }
    public void OnMMEvent(DemoChannel eventType)
    {
        Debug.LogError(eventType.State.ToString());
        
        switch(eventType.State)
        {
            case DemoConstValue.Intro:
                fSMSystem.GotoState(DemoConstValue.Intro.ToString(), "");
                break;
            default:
                return;
        }    
    }
}
