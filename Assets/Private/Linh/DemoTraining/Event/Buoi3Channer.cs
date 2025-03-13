using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Buoi3Channer : EventListener<Buoi3Channer>
{
    public Buoi3StatusOfStateState State;
    public object Data;

    public Buoi3Channer(Buoi3StatusOfStateState state, object data)
    {
        this.State = state;
        this.Data = data;
    }

    public void OnMMEvent(Buoi3Channer eventType)
    {
       
    }
}
