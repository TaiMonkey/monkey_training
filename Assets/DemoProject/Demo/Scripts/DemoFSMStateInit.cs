using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoFSMStateInit : FSMState
{
    private DemoInitStateObjectDependency dependency;

    public override void SetUp(object data)
    {
        dependency = (DemoInitStateObjectDependency)data;
    }

    public override void OnEnter(object data)
    {
        base.OnEnter();
        DemoGamePlayData1 demoGamePlayData = (DemoGamePlayData1)data;
        dependency.sprites[0].sprite = demoGamePlayData.demoData.sprites[0];
        dependency.sprites[1].sprite = demoGamePlayData.demoData.sprites[1];
        dependency.sprites[2].sprite = demoGamePlayData.demoData.sprites[2];
        Debug.LogError(demoGamePlayData.demoData.texts[0]);
    }
}

public class DemoInitStateObjectDependency
{
    public List<Image> sprites { get; set; }
}
