using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoDependency : Dependency
{
    [SerializeField] private DemoConfig demoConfig;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Button buttonSpeaker;

    public override T GetStateData<T>()
    {
        T data;

        Type listType = typeof(T);
        if(listType == typeof(InitStateObjectDependency))
        {
            InitStateObjectDependency initData = new InitStateObjectDependency();
            initData.IntroConfig = demoConfig.introConfig;
            initData.ButtonList = buttons;
            initData.SpeakerButton = buttonSpeaker;

            data = ConvertToType<T>(initData);
        } 
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }

}
