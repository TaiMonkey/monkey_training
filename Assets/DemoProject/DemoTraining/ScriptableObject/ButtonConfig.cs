using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName ="ButtonConfig", menuName ="Game/Demo2/Setting")]
public class ButtonConfig : ScriptableObject
{
    public ButtonColor colorNormal;
    public ButtonColor colorCorrect;
    public ButtonColor colorWrong;
}

[Serializable]
public class ButtonColor
{
    public Color32 backround;
    public Color32 shadow;
}
