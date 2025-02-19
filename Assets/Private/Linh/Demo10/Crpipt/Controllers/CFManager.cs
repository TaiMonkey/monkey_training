using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFManager : GameManager
{
    protected override void Awake()
    {
        base.Awake();
        string dataServerFake = "";
        SetData(dataServerFake);
    }
    protected override void Start()
    {
        MyMethod();
    }
    void MyMethod()
    {
        fSMSystem.SetupStateData(dependency);
        fSMSystem.GotoState("Init", adapter.GetData<CFInitStateData>(0));
    }
    public override void SetData<T>(T data)
    {
        base.SetData(data);
        adapter.SetData(data);
    }
}
