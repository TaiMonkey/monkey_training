using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BELT01MCPInitState : FSMState
{
    
    private BELT01MCPInitStateObjectDependency dependency;
   

    public override void SetUp(object data)
    {
        dependency = (BELT01MCPInitStateObjectDependency)data;
        Debug.LogError(dependency.Buttons.Count);
    }
    public override void OnEnter(object data)
    {
        /* base.OnEnter(data);
         BELT01MCPInitStateData bELT01MCPInitStateData = (BELT01MCPInitStateData)data;
          listButton = new List<ButtonDemo1>();
         for(int i = 0; i < 3; i++)
         {
             GameObject obj = GameObject.Instantiate(gameObject, layoutGroup.transform, false);
             obj.transform.localScale = Vector3.one;
             ButtonDemo1 buttonDemo1 = obj.GetComponent<ButtonDemo1>();
             buttonDemo1.InitData(bELT01MCPInitStateData.CurrentTurn.listButton[i]);
             listButton.Add(buttonDemo1);
         }
         */
        //Debug.LogError($"OnEnter has Data: {data}");
        base.OnEnter(data);
        BELT01MCPInitStateData bELT01GamePlayData = (BELT01MCPInitStateData)data;
        
        for(int i = 0; i < bELT01GamePlayData.CurrentTurn.listButton.Count; i++)
        {
            BELT01ButtonData dataButton = bELT01GamePlayData.CurrentTurn.listButton[i];

            dependency.Buttons[i].InitData(dataButton, dependency.ButtonSpeaker.GetComponent<RectTransform>());   
        }
        foreach(var buttonans in dependency.Buttons)
        {
            buttonans.transform.localScale = Vector3.zero;
        }
        MyMethod();
    }
    private void MyMethod()
    {
        BELT01MCPDataChanner bELT01MCPDataChanner = new BELT01MCPDataChanner(BELT01MCPStatusOfStateState.InitStateEnd, "dfgay");
        ObserverManager.TriggerEvent(bELT01MCPDataChanner);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}


public class BELT01MCPInitStateData
{
    public BELT01Turn CurrentTurn { get; set; }
    public string DataEvent { get; set; }
}
public class BELT01MCPInitStateObjectDependency
{
    public List<ButtonDemo1> Buttons { get; set; }
    public Button ButtonSpeaker { get; set; }
}