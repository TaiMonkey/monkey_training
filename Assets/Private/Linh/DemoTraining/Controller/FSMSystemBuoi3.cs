using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystemBuoi3 : FSMSystem
{
    private InitStateBuoi3 initStateBuoi3;
    private IntroStateBuoi3 introStateBuoi3;
    private DraggingStateBuoi3 draggingStateBuoi3;
    private DragResultBuoi3 dragResultBuoi3;
    private GuidingStateBuoi3 guidingStateBuoi3;

    private void Awake()
    {
        initStateBuoi3 = new InitStateBuoi3();
        introStateBuoi3 = new IntroStateBuoi3();
        dragResultBuoi3 = new DragResultBuoi3();
        draggingStateBuoi3 = new DraggingStateBuoi3();
        guidingStateBuoi3 = new GuidingStateBuoi3();
    }

  
    public override void SetupStateData<T>(T data)
    {
        if(data is Dependency dependency){
            InitStateBuoi3ObjectDependency initStateData = dependency.GetStateData<InitStateBuoi3ObjectDependency>();
            initStateBuoi3.SetUp(initStateData);

            IntroStateBuoi3ObjectDependency introStateData = dependency.GetStateData<IntroStateBuoi3ObjectDependency>();
            introStateBuoi3.SetUp(introStateData);

            DraggingStateBuoi3ObjectDependency draggingStateData = dependency.GetStateData<DraggingStateBuoi3ObjectDependency>();
            draggingStateBuoi3.SetUp(draggingStateData);
            
            DragResultBuoi3ObjectDependency dragStateData = dependency.GetStateData<DragResultBuoi3ObjectDependency>();
            dragResultBuoi3.SetUp(dragStateData);

            BELT01MCPGuidingStateDependency guidingStateData = dependency.GetStateData<BELT01MCPGuidingStateDependency>();
            guidingStateBuoi3.SetUp(guidingStateData);
        }
    }

    public override void GotoState(string eventName, object data)
    {
        Buoi3State state = (Buoi3State)Enum.Parse(typeof(Buoi3State), eventName);
        switch (state)
        {
            case Buoi3State.InitData:
                GotoState(initStateBuoi3, data);
                break;
            case Buoi3State.Intro:
                GotoState(introStateBuoi3, data);
                break;
            case Buoi3State.Draggingg:
                GotoState(draggingStateBuoi3, data);
                break;
            case Buoi3State.DragResult:
                GotoState(dragResultBuoi3, data);
                break;
            case Buoi3State.Guiding:
                GotoState(guidingStateBuoi3, data);
                break;
            default:
                return;
        }
    }

}
   
