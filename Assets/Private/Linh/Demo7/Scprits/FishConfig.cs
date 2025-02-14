using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FishConfig", menuName = "Game/Demo7/Setting")]
public class FishConfig : ScriptableObject
{
    public string idle;
    public string userUntap;
    public string usertap;
    public string userTaploop;
    public List<string> listSkins;
}



