using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELT01MCPSystem : FSMSystem
{
    private BELT01MCPInitState bELT01MCPInit;
    private void Awake()
    {
        bELT01MCPInit = new BELT01MCPInitState();
    }
    public override void SetupStateData<T>(T data)
    {
       if(data is Dependency dependency)
        {
            BELT01MCPInitStateObjectDependency initStateData = dependency.GetStateData<BELT01MCPInitStateObjectDependency>();
            bELT01MCPInit.SetUp(initStateData);
        }
    }
    public override void GotoState(string eventName, object data)
    {
        base.GotoState(eventName, data);
    }


}
