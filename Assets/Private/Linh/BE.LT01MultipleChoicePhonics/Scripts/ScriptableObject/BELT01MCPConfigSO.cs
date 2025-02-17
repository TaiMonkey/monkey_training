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
    public BERLT01MCPGuidingConfig guidingConfig;
    public AnmationPhaohoa anmationPhaohoa;

}
[Serializable]
public class BELT01MCPIntroConfig
{
    public List<AudioClip> sfxCTA;
    public List<AudioClip> sfxPopup;
    public List<int> timeDelay;
    public float timeFadein;

}

[Serializable]
public class BELT01MCPClickConfig
{
    public AudioClip sfxClick;
    public AudioClip sfxCorrect;
    public AudioClip sfxWrong;
    public List<int> timeDelay;
   
}
[Serializable]
public class BERLT01MCPGuidingConfig
{
    public AudioClip sfxClick;
    public AudioClip sfxguiding;
    public int timeWaitStartGuiding;
    public float timeFadein;
    public float timeFadeout;
}
[Serializable]
public class AnmationPhaohoa
{
    public List<string> animPhaohoa;
}




