using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BELT01MCPDependency : Dependency
{
    [SerializeField] private List<ButtonDemo1> listButton;
    [SerializeField] private Button buttonSpeaker;
    [SerializeField] private BELT01MCPConfigSO bELT01MCPConfigSO;


    public override T GetStateData<T>()
    {
        T data;
        Type typeData = typeof(T);
        if (typeData == typeof(BELT01MCPInitStateObjectDependency)) 
        {
            BELT01MCPInitStateObjectDependency bELT01MCPInitStateObjectDependency = new BELT01MCPInitStateObjectDependency();
            bELT01MCPInitStateObjectDependency.Buttons = listButton;
            bELT01MCPInitStateObjectDependency.ButtonSpeaker = buttonSpeaker;
            data = ConvertToType<T>(bELT01MCPInitStateObjectDependency);
        }
        else if (typeData == typeof(BELT01MCPIntroStateObjectDependency))
        {
            BELT01MCPIntroStateObjectDependency bELT01MCPIntroStateObjectDependency = new BELT01MCPIntroStateObjectDependency();
            bELT01MCPIntroStateObjectDependency.IntroConfig = bELT01MCPConfigSO.introConfig;
            bELT01MCPIntroStateObjectDependency.listButtonans = listButton;
            data = ConvertToType<T>(bELT01MCPIntroStateObjectDependency);
        }
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }

   
}
