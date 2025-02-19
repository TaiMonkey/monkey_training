using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoFSMStateInit : FSMState
{
    private DemoInitStateObjectDependency dependency;
    private DemoConstValue state;

    public override void SetUp(object data)
    {
        dependency = (DemoInitStateObjectDependency)data;
    }

    public override void OnEnter(object data)
    {
        base.OnEnter();
        //DemoGamePlayData1 demoGamePlayData = (DemoGamePlayData1)data;
        foreach(var answer in dependency.buttonAnswers)
        {
            answer.transform.localScale = Vector3.zero;
        }
        ButtonSpeaker buttonSpeaker = dependency.buttonSpeaker;
        endInit();
    }

    public void endInit()
    {
        DemoChannel demoChannel = new DemoChannel(DemoConstValue.Intro, "Start Intro");
        ObserverManager.TriggerEvent(demoChannel);
    }
}

public class DemoInitStateObjectDependency
{
    public List<ButtonAnswer> buttonAnswers  {get; set; }
    public ButtonSpeaker buttonSpeaker { get; set; }
}
