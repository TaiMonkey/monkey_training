using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELT01MCPSystem : FSMSystem
{
    private BELT01MCPInitState bELT01MCPInit;
    private BELT01MCPIntroState bELT01MCPIntroState;
    private BELT01MCPDragResult bELT01MCPDragResult;
    private BELT01MCPGuidingState bELT01MCPGuidingState;
    private BELT01MCPDraggingState bELT01MCPDraggingState;
    private void Awake()
    {
        bELT01MCPInit = new BELT01MCPInitState();
        bELT01MCPIntroState = new BELT01MCPIntroState();
        bELT01MCPDragResult = new BELT01MCPDragResult();
        bELT01MCPGuidingState = new BELT01MCPGuidingState();
        bELT01MCPDraggingState = new BELT01MCPDraggingState();
    }
    public override void SetupStateData<T>(T data)
    {
       if(data is Dependency dependency)
        {
            BELT01MCPInitStateObjectDependency initStateData = dependency.GetStateData<BELT01MCPInitStateObjectDependency>();
            bELT01MCPInit.SetUp(initStateData);
            BELT01MCPIntroStateObjectDependency introStateData = dependency.GetStateData<BELT01MCPIntroStateObjectDependency>();
            bELT01MCPIntroState.SetUp(introStateData);
            BELT01MCPDBELT01MCPDraggingStateDependency dragdingStateData = dependency.GetStateData<BELT01MCPDBELT01MCPDraggingStateDependency>();
            bELT01MCPDraggingState.SetUp(dragdingStateData);
            BELT01MCPDragResultObjectDependency dragResulStateData = dependency.GetStateData<BELT01MCPDragResultObjectDependency>();
            bELT01MCPDragResult.SetUp(dragResulStateData);
            BELT01MCPGuidingStateDependency guidingStateData = dependency.GetStateData<BELT01MCPGuidingStateDependency>();
            bELT01MCPGuidingState.SetUp(guidingStateData);
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
            case BELT01MCPState.Dragging:
                GotoState(bELT01MCPDraggingState, data);
                break;
            case BELT01MCPState.DragResult:
                GotoState(bELT01MCPDragResult, data);
                break;
            case BELT01MCPState.Guiding:
                GotoState(bELT01MCPGuidingState, data);
                break;
            default:
                return;
        }
    }

}
