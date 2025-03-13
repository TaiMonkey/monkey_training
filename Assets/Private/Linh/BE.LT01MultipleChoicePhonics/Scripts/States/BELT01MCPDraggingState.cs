using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELT01MCPDraggingState : FSMState
{
    private BELT01MCPDBELT01MCPDraggingStateDependency dependency;
    public override void SetUp(object data)
    {
        dependency = (BELT01MCPDBELT01MCPDraggingStateDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter();
        ButtonDemo1 currentButton = (ButtonDemo1)data;
        foreach (var item in dependency.listButtons)
        {
            if (item != currentButton)
            {
                item.IsEnable = false;
            }
        }
    }
}
public class BELT01MCPDBELT01MCPDraggingStateDependency
{
    public List<ButtonDemo1> listButtons;
}


