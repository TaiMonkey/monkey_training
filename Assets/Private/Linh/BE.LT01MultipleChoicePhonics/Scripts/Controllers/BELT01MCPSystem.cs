using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELT01MCPSystem : FSMSystem
{
    private BELT01MCPInitState bELT01MCPInit;
    private BELT01MCPIntroState bELT01MCPIntroState;
    private void Awake()
    {
        bELT01MCPInit = new BELT01MCPInitState();
        bELT01MCPIntroState = new BELT01MCPIntroState();
    }
    public override void SetupStateData<T>(T data)
    {
       if(data is Dependency dependency)
        {
            BELT01MCPInitStateObjectDependency initStateData = dependency.GetStateData<BELT01MCPInitStateObjectDependency>();
            bELT01MCPInit.SetUp(initStateData);
            BELT01MCPIntroStateObjectDependency introStateData = dependency.GetStateData<BELT01MCPIntroStateObjectDependency>();
            bELT01MCPIntroState.SetUp(introStateData); 
        }
        

       
    }
    public override void GotoState(string eventName, object data)
    {
        BELT01MCPState state = (BELT01MCPState)Enum.Parse(typeof(BELT01MCPState), eventName);
        switch (state)
        {
            case BELT01MCPState.InitData:
                GotoState(bELT01MCPInit, data);
                break;
            case BELT01MCPState.Intro:
                GotoState(bELT01MCPIntroState, data);
                break;
            default:
                return;
        }
    }

}
