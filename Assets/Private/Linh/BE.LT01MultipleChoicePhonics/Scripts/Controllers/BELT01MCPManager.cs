using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELT01MCPManager : GameManager, EventListener<BELT01MCPDataChanner>
{
    protected override void Awake()
    {
        base.Awake();
        string dataServerFake = "";
        SetData(dataServerFake);
    }
    protected override void Start()
    {
        MyMethod();
    }
    void MyMethod()
    {
        fSMSystem.SetupStateData(dependency);
        fSMSystem.GotoState(BELT01MCPState.InitData.ToString(), adapter.GetData<BELT01MCPInitStateData>(0));
    }
    public override void SetData<T>(T data)
    {
        base.SetData(data);
        adapter.SetData(data);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.ObserverStopListening<BELT01MCPDataChanner>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ObserverStartListening<BELT01MCPDataChanner>();
    }


    public void OnMMEvent(BELT01MCPDataChanner eventType)
    {
        Debug.Log("Linh Do: " + eventType.State.ToString());
        switch (eventType.State)
        {
            case BELT01MCPStatusOfStateState.InitStateStart:
                BELT01MCPInitStateData bELT01MCPInitStateData = new BELT01MCPInitStateData();
                bELT01MCPInitStateData.CurrentTurn = adapter.GetData<BELT01Turn>(0);
                bELT01MCPInitStateData.DataEvent = (string)eventType.Data;
                fSMSystem.GotoState(BELT01MCPState.InitData.ToString(), bELT01MCPInitStateData);
                break;
            case BELT01MCPStatusOfStateState.InitStateEnd:
            case BELT01MCPStatusOfStateState.IntroStateStart:
                string dataInitSend = (string)eventType.Data;
                fSMSystem.GotoState(BELT01MCPState.Intro.ToString(), dataInitSend);
                break;
            case BELT01MCPStatusOfStateState.IntroStateEnd:
            case BELT01MCPStatusOfStateState.GuidingStateStart:
                fSMSystem.GotoState(BELT01MCPState.Guiding.ToString(), null);
                break;
            default:
                return;





        }
    }
}
