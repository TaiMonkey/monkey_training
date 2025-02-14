using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BELT01MCPConfigSO", menuName = "ScriptableObjects/BELT01MultipleChoicePhonics/Setting", order = 1)]
public class BELT01MCPConfigSO : ScriptableObject
{
    public AudioClip audioBackground;
    public BELT01MCPIntroConfig introConfig;
    

}
[Serializable]
public class BELT01MCPIntroConfig
{
    public List<AudioClip> sfxCTA;
    public List<AudioClip> sfxPopup;
    public int timeDelay;
}

[Serializable]
public class BELT01MCPClickConfig
{
    public AudioClip sfxCorrect;
    public AudioClip sfxWrong;
}
[Serializable]
public class BERLT01MCPGuidingConfig
{
    public AudioClip sfxAppear;
    public AudioClip sfxClick;
    public AudioClip sfxUnClick;
    public int timeWaitStartGuiding;
    public int timeDelay;
}

