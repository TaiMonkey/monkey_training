using UnityEngine;
using System;

namespace Monkey.Game.StoryElement
{
    [CreateAssetMenu(fileName = "StoryElement", menuName = "ScriptableObjects/StoryElement/ConfigDataState", order = 1)]
    public class ConfigDataState : ScriptableObject
    {
        public InitConfig InitConfig;
    }

    [Serializable]
    public class InitConfig
    {
        public AudioClip Bg_audio;
        public AudioClip AudioCTA;
        public int Delay500;
        public int Delay250;
    }
}
