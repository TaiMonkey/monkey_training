using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoDependency1 : Dependency
{
    [SerializeField] private List<ButtonAnswer> buttonAnswers;
    [SerializeField] private ButtonSpeaker buttonSpeaker;
    [SerializeField] private DemoMockData demoMockData;

    public override T GetStateData<T>()
    {
        T data;
        Type listType = typeof(T);
        if(listType == typeof(DemoInitStateObjectDependency))
        {
            DemoInitStateObjectDependency initData = new DemoInitStateObjectDependency();
            initData.buttonAnswers = buttonAnswers;
            initData.buttonSpeaker = buttonSpeaker;
            data = ConvertToType<T>(initData);
        }
        else if(listType == typeof(DemoStateIntroDependency))
        {
            DemoStateIntroDependency introData = new DemoStateIntroDependency();
            introData.buttonSpeaker = buttonSpeaker;
            introData.audioCta = demoMockData.mockDataGamePlay.ListTurn[0].demoData.audioCta;
            introData.listAnswer = buttonAnswers;
            introData.buttonSpeaker = buttonSpeaker;
            introData.listAudioPopup = demoMockData.audioPopups;
            data = ConvertToType<T>(introData);
        }
            
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }
}
