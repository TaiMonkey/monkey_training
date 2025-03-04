using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.MJ5.BESTW01SortTheWords
{
    [CreateAssetMenu(fileName = "BESTW01ConfigSO", menuName = "ScriptableObjects/BESTW01SortTheWords/Setting", order = 1)]

    public class BESTW01ConfigSO : ScriptableObject
    {
        public AudioClip audioBackground;
        public BESTW01IntroConfig introConfig;
        public BESTW01GuidingConfig guidingConfig;
        public BESTW01SDragResultConfig dragResultConfig;
        public BESTW01SEndGameConfig endGameConfig;
        public BESTW01CardConfig cardConfig;
        public BESTW01BoxConfig boxConfig;
    }


    [Serializable]
    public class BESTW01IntroConfig
    {
        public AudioClip[] audiosTopic;
        public int timeDelay;
    }

    [Serializable]
    public class BESTW01GuidingConfig
    {
        public AudioClip sfxAppear;
        public AudioClip sfxClick;
        public AudioClip sfxUnClick;
        public float secondWaitStartGuiding;
        public float secondDelay;
    }
    [Serializable]
    public class BESTW01SDragResultConfig
    {
        public AudioClip sfxCorrect;
        public AudioClip sfxWrong;
        public int timeDelay;
    }

    [Serializable]
    public class BESTW01SEndGameConfig
    {
        public AudioClip sfxYeah;
        public int timeDelay;
    }
    [Serializable]
    public class BESTW01CardConfig
    {
        public string cardNormal;
        public string tapCard;
        public string cardFlyToBox;
        public string unTapFirstCard;
        public string unTapSecondCard;
        public AudioClip sfxClick; 
        public AudioClip sfxUnClick;
    }


    [Serializable]
    public class BESTW01BoxConfig
    {
        public string boxNormal;
        public string boxCorrect;
        public string boxWrong;
        public string boxPopup;
        public string boxTap;
        public string boxGreen;
        public string boxGreenTap;
        public string[] fireworks;
        public AudioClip sfxSelect;
    }
}