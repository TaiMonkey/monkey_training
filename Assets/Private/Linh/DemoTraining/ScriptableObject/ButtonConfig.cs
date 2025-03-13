using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName ="DemoButtonConfig", menuName ="Game/Demo2/Setting")]
public class DemoTrainingButtonConfig : ScriptableObject
{
    public DemoButtonColor colorNormal;
    public DemoButtonColor colorCorrect;
    public DemoButtonColor colorWrong;
}

[Serializable]
public class DemoButtonColor
{
    public Color32 backround;
    public Color32 shadow;
}
