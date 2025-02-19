using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFInitState : FSMState
{
    private CFInitStateObjectDependency dependency;
    public override void SetUp(object data)
    {
        dependency = (CFInitStateObjectDependency)data;
    }
    public override void OnEnter(object data)
    {
        base.OnEnter();
        CFInitStateData cFGamePlayData = (CFInitStateData)data;

        for (int i = 0; i < cFGamePlayData.CurrentTurn.listButtonDomdom.Count; i++)
        {
            CFButtonDomDomData dataButtonDomdom = cFGamePlayData.CurrentTurn.listButtonDomdom[i];
            dependency.ButtonsDomdom[i].InitData(dataButtonDomdom);
          
        }
        for (int i = 0; i < cFGamePlayData.CurrentTurn.listButtonJar.Count; i++)
        {
            CFButtonJarData dataButtonJar = cFGamePlayData.CurrentTurn.listButtonJar[i];
            dependency.ButtonsJar[i].InitData(dataButtonJar);

        }
    }

}
    public class CFInitStateData
{
    public CFTurn CurrentTurn { get; set; }
}
public class CFInitStateObjectDependency
{
    public List<ButtonJarController> ButtonsJar { get; set; }
    public List<ButtonDomdomCotroller> ButtonsDomdom { get; set; }
}

