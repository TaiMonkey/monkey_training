using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFSMSystem : FSMSystem
{
    private DemoFSMStateInit demoFSMStateInit;
    private DemoFSMStateIntro demoFSMStateIntro;
    private DemoFSMStateDrag demoFSMStateDrag;

    private void Awake()
    {
        demoFSMStateInit = new DemoFSMStateInit();
        demoFSMStateIntro = new DemoFSMStateIntro();
        demoFSMStateDrag = new DemoFSMStateDrag();
    }

    public override void SetupStateData<T>(T data)
    {  
        if(data is Dependency dependency)
        {
            //Init
            DemoInitStateObjectDependency demoInitStateObjectDependency = dependency.GetStateData<DemoInitStateObjectDependency>();
            demoFSMStateInit.SetUp(demoInitStateObjectDependency);

            //Intro
            DemoStateIntroDependency demoStateIntroDependency = dependency.GetStateData<DemoStateIntroDependency>();
            demoFSMStateIntro.SetUp(demoStateIntroDependency);

            //Drag
            DemoFSMStateDragDependency demoFSMStateDragDependency = dependency.GetStateData<DemoFSMStateDragDependency>();
            demoFSMStateDrag.SetUp(demoFSMStateDragDependency);
        }
    }
    public override void GotoState(string eventName, object data)
    {
        DemoConstValue state = (DemoConstValue)Enum.Parse(typeof(DemoConstValue), eventName);
        switch(state)
        {
            case DemoConstValue.Init:
                GotoState(demoFSMStateInit, data);
                break;
            case DemoConstValue.Intro:
                GotoState(demoFSMStateIntro, data);
                break;
            case DemoConstValue.DragStart:
                GotoState(demoFSMStateDrag, data);
                break;
            default:
                return;
        }    
    }
}
