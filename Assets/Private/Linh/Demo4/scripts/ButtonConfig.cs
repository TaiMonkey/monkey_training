using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ButtonConfig", menuName = "Game/Demo4/Setting")]
public class DemoButtonConfig : ScriptableObject
{
    public ButtonColor buttonNomal;
    public ButtonColor buttonFalse;
    public ButtonColor buttonTrue;
    public float scale;
    
}


[Serializable]
public class ButtonColor{
    public Color32 colorBackgound;
    public Color32 colorShadow;
}


