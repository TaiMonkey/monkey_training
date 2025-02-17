using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BELT01MCPInitState : FSMState
{
    private BELT01MCPInitStateData bELT01MCPInitStateData;

    private List<ButtonDemo1> listButton;
    private GameObject gameObject;
    private HorizontalLayoutGroup layoutGroup;
    private BELT01MCPInitStateObjectDependency dependency;

    public override void SetUp(object data)
    {
        dependency = (BELT01MCPInitStateObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
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
    }
}
public class BELT01MCPInitStateData
{
    public BELT01Turn CurrentTurn { get; set; }
}
public class BELT01MCPInitStateObjectDependency
{
    public List<Button> Buttons { get; set; }
    public Button ButtonSpeaker { get; set; }
}