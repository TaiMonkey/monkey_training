using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameManager : GameManager
{
    protected override void Start()
    {
        base.Start();
        string data = "";
        fSMSystem.SetupStateData(dependency);
        adapter.SetData(data);
        fSMSystem.GotoState("Init", adapter.GetData<DemoGamePlayData1>(0));
    }
}
