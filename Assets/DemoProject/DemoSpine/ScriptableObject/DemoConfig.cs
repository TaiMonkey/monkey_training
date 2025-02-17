using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;

[CreateAssetMenu(fileName = "demoConfig", menuName= "ScriptableObject/demoConfig")]
public class DemoConfig : ScriptableObject
{
    public AudioClip audioBackGround;
    public IntroConfig introConfig;
    public ClickConfig clickConfig;
    public GuidingConfig guidingConfig;
    public Fade fadeConfig;
}

[Serializable]
public class IntroConfig
{
    public List<AudioClip> audioCTA;
    public List<String> animPhaohoa;
    public List<AudioClip> sfxPopup;
    public int timeDelay200;
    public int timeDelay100;
}

[Serializable]
public class ClickConfig
{
    public AudioClip sfxCorrect;
    public AudioClip sfxWrong;
    public AudioClip sfxClick;
}

[Serializable]
public class GuidingConfig
{
    public AudioClip sfxGuiding;
    public int timeWaitStartGuiding;
}

[Serializable]
public class Fade
{
    public float fade100;
    public float fade200;
    public float fade300;
}
