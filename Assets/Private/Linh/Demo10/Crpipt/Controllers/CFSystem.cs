using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFSystem : FSMSystem
{ 

    private CFInitState cFInit;
    private void Awake()
    {
        cFInit = new CFInitState();
    }
    public override void SetupStateData<T>(T data)
    {
        if (data is Dependency dependency)
        {
            CFInitStateObjectDependency initStateData = dependency.GetStateData<CFInitStateObjectDependency>();
            cFInit.SetUp(initStateData);
        }
    }
    public override void GotoState(string eventName, object data)
    {
        base.GotoState(eventName, data);

        if (eventName == "Init")
        {
            GotoState(cFInit, data);
        }
    }

}
