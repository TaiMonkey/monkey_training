using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    [CreateAssetMenu(fileName = "BreakTheEgg", menuName = "ScriptableObjects/BreakTheEgg/ConfigDataState", order = 1)]
    public class ConfigDataState : ScriptableObject
    {
        public InitConfig initConfig;
        public IntroConfig introConfig;
        public SkinConfig skinConfig;
        public GameplayConfig gameplayConfig;
    }

    [Serializable]
    public class InitConfig
    {
        public AudioClip Bg_audio;
        public AudioClip AudioCTA;
        public int Delay500;
        public int Delay3000;
    }

    [Serializable]
    public class IntroConfig
    {
        public AudioClip InstructionCTA;
        public int TimeDelay;
        public AudioClip SfxJump;
    }

    [Serializable]
    public class GameplayConfig
    {
        public AudioClip SfxTextShow;
        public int Delay300;
        public int Delay350;
        public int Delay3000;

    }

    [Serializable]
    public class SkinConfig
    {
        public SkinName EggA;
        public SkinName EggB;
        public SkinName EggC;
    }

    [Serializable]
    public class SkinName
    {
        public string EggNormal;
        public string Tap1;
        public string Tap1Normal;
        public string Tap2;
        public string Tap2Normal;
        public string Tap3;
        public string Tap3Normal;
        public string Tap4;
        public string Tap4Normal;
        public string Tap5;
        public string Tap5Normal;
        public string Tap6;
        public string Tap6Normal;
        public string Tap7;
        public string Tap7Normal;
        public string Tap8;
        public string Tap8Normal;
        public string Tap9;
        public string Tap9Normal;
        public string Tap10;
    }
}
