using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DemoChannel : EventListener<DemoChannel>
{

    public DemoConstValue State;
    public object Data;

    public DemoChannel(DemoConstValue state, object data)
    {
        this.State = state;
        this.Data = data;
    }

    public void OnMMEvent(DemoChannel eventType)
    {
        throw new System.NotImplementedException();
    }
}
