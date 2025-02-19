using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BELT01MCPDataChanner : EventListener<BELT01MCPDataChanner>
{
    public BELT01MCPStatusOfStateState State;
    public object Data;
    public BELT01MCPDataChanner(BELT01MCPStatusOfStateState state,object data)
    {
        this.State = state;
        this.Data =  data;
    }
    public void OnMMEvent(BELT01MCPDataChanner eventType)
    {
    }
}
