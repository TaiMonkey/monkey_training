using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DemoConfigSO", menuName = "Game/DemoBuoi3/ConfigSetting")]
public class DemoConfigSO : ScriptableObject
{
    public DemoIntroConfig demoIntroConfig;
    public DemoGuidingConfig demoGuidingConfig;
}


[Serializable]
public class DemoIntroConfig
{
    public AudioClip audioCta;
    public int timeDelay;
}


[Serializable]
public class DemoGuidingConfig
{
    public AudioClip sfxAppear;
    public AudioClip sfxClick;
    public AudioClip sfxUnClick;
    public int timeDelayStart;
    public int timeDelay;
}
