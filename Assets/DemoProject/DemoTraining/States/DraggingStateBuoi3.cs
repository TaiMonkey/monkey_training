using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingStateBuoi3 : FSMState
{
    private DraggingStateBuoi3ObjectDependency dependency;
    public override void SetUp(object data)
    {
        dependency = (DraggingStateBuoi3ObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        DemoFishButton currentButtonFish = (DemoFishButton)data;
       foreach(var item in dependency.ListFish)
        {
            if(item != currentButtonFish)
            {
                item.IsEnable = false;
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}

public class DraggingStateBuoi3ObjectDependency
{
    public List<DemoFishButton> ListFish { get; set; }

}
