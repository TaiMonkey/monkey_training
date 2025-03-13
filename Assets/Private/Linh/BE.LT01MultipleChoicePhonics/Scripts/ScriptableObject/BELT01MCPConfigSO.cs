using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BELT01MCPConfigSO", menuName = "ScriptableObjects/BELT01MultipleChoicePhonics/Setting", order = 1)]
public class BELT01MCPConfigSO : ScriptableObject
{
    public AudioClip audioBackground;
    public BELT01MCPIntroConfig introConfig;
    public BELT01MCPClickConfig clickconfig;
   
    public AnmationPhaohoa anmationPhaohoa;
    public BELT01GuidingConfig bELT01GuidingConfig;

}
[Serializable]
public class BELT01MCPIntroConfig
{
    public List<AudioClip> sfxCTA;
    public List<AudioClip> sfxPopup;
    public int timeDelay;
    public int timeFadein;

}

[Serializable]
public class BELT01MCPClickConfig
{
    public AudioClip sfxClick;
    public AudioClip sfxCorrect;
    public AudioClip sfxWrong;
    public int timeDelay;
   
}
[Serializable]
public class BELT01GuidingConfig
{
    public AudioClip sfxAppear;
    public AudioClip sfxClick;
    public AudioClip sfxUnClick;
    public int timeDelayStart;
    public int timeDelay;
}
[Serializable]
public class AnmationPhaohoa
{
    public List<string> animPhaohoa;
}






