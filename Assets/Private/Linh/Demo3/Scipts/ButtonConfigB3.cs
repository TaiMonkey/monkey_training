using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ButtonConfig", menuName = "Game/Demo3/Setting")]
public class ButtonConfigB3 : ScriptableObject
{
    public ButtonColor1 buttonNomal;
    public ButtonColor1 buttonFalse;
    public ButtonColor1 buttonTrue;
    public float scale;

}


[Serializable]
public class ButtonColor1
{
    public Color32 colorBackgound;
    public Color32 colorShadow;
}
