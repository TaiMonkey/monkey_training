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
    [SerializeField] private Transform buttonGroup;
    [SerializeField] private CanvasGroup uiGuiding;
    [SerializeField] private Image handLong;
    [SerializeField] private Image handShort;
 
 

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
        else if (typeData == typeof(BELT01MCPDBELT01MCPDraggingStateDependency))
        {
            BELT01MCPDBELT01MCPDraggingStateDependency bELT01MCPDraggingDependency = new BELT01MCPDBELT01MCPDraggingStateDependency();
            bELT01MCPDraggingDependency.listButtons = listButton;
            data = ConvertToType<T>(bELT01MCPDraggingDependency);

        }
        else if (typeData == typeof(BELT01MCPDragResultObjectDependency))
        {
            BELT01MCPDragResultObjectDependency bELT01MCPDragResultObjectDependency = new BELT01MCPDragResultObjectDependency();
            bELT01MCPDragResultObjectDependency.TransSpeaker = buttonSpeaker.transform;
            
            data = ConvertToType<T>(bELT01MCPDragResultObjectDependency);

        }
        else if (typeData == typeof(BELT01MCPGuidingStateDependency))
        {
            BELT01MCPGuidingStateDependency bELT01MCPGuidingStateDependency = new BELT01MCPGuidingStateDependency();
            bELT01MCPGuidingStateDependency.bELT01GuidingConfig = bELT01MCPConfigSO.bELT01GuidingConfig;
            bELT01MCPGuidingStateDependency.ListButton = listButton;
            bELT01MCPGuidingStateDependency.TransSpeak = buttonSpeaker.transform;
            bELT01MCPGuidingStateDependency.ButtonGroup = buttonGroup;
            bELT01MCPGuidingStateDependency.UiGuiding = uiGuiding;
            bELT01MCPGuidingStateDependency.HandLong = handLong;
            bELT01MCPGuidingStateDependency.HandShort = handShort;

            data = ConvertToType<T>(bELT01MCPGuidingStateDependency);
        }
        else if (typeData == typeof(BELT01MCPEndStateDependency))
        {
            BELT01MCPEndStateDependency bELT01MCPEndStateDependency = new BELT01MCPEndStateDependency();

            bELT01MCPEndStateDependency.Buttons = listButton;
            bELT01MCPEndStateDependency.buttonSpeak = buttonSpeaker;
            data = ConvertToType<T>(bELT01MCPEndStateDependency);
        }

        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }

   
}
