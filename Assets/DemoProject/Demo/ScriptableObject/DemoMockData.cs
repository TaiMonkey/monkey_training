using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "demo", menuName = "demo/demo")]
public class DemoMockData : ScriptableObject
{
    public DemoGamePlayData1 mockDataGamePlay;
    public List<AudioClip> audioPopups;
}
