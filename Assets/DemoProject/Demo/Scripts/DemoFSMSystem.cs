using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFSMSystem : FSMSystem
{
    private DemoFSMStateInit demoFSMStateInit;

    private void Awake()
    {
        demoFSMStateInit = new DemoFSMStateInit();
    }

    public override void SetupStateData<T>(T data)
    {  
        if(data is Dependency dependency)
        {
            DemoInitStateObjectDependency demoInitStateObjectDependency = dependency.GetStateData<DemoInitStateObjectDependency>();
            demoFSMStateInit.SetUp(demoInitStateObjectDependency);
        }
    }
    public override void GotoState(string eventName, object data)
    {
        base.GotoState(eventName, data);
        if(eventName == "Init")
        {
            GotoState(demoFSMStateInit, data);
        }
    }
}
