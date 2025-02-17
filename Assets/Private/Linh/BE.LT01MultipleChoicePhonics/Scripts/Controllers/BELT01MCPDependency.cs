using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BELT01MCPDependency : Dependency
{
    [SerializeField] private List<Button> listButton;
    [SerializeField] private Button buttonSpeaker;
   

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
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }

   
}
