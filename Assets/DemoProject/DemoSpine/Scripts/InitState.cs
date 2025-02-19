using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitState : FSMState
{
    private InitStateObjectDependency dependency;
    public override void SetUp(object data)
    {
        dependency = (InitStateObjectDependency)(data);
    }

    public override void OnEnter(object data)
    {
        base.OnEnter(data);
    }

}

public class InitStateData
{
    public GamePlayData1 listData { get; set;}
}

public class InitStateObjectDependency
{
    public IntroConfig IntroConfig { get; set; }
    public List<Button> ButtonList { get; set; }
    public Button SpeakerButton { get; set; }
}
