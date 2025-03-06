using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Monkey.Game.GameTest
{
    [CreateAssetMenu(fileName = "GameTestConfig", menuName = "ScriptableObjects/GameTest/GameTestConfig", order = 1)]
    public class GameTestConfig : ScriptableObject
    {
        public IntroConfig IntroConfig;
        public AnimalButtonConfig AnimalButton;
        public GameTestAnimalConfig AnimConfig;
        public EndGameConfig EndGameConfig;
    }

    [Serializable]
    public class AnimalButtonConfig
    {
        public AudioClip SfxChoose;
        public AudioClip SfxCorrect;
        public AudioClip SfxWrong;
        public float SizeIncrease;
        public float SizeDecrease;
        public float SpeedIncrease;
    }

    [Serializable]
    public class IntroConfig
    {
        public int TimeDelay;
        public AudioClip AudioCTA;
    }

    [Serializable]
    public class GameTestAnimalConfig
    {
        public string animNomal;
        public string animKeo;
        public string animkeo_loop;
        public string tha;
        public string vui_mung;
    }

    [Serializable]
    public class EndGameConfig
    {
        public AudioClip SfxCheer;
    }
}