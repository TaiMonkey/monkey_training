using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    [Serializable]
    public class DataConfig
    {
        public List<ConfigEgg> ConfigEgg;
    }

    [Serializable]
    public class ConfigEgg
    {
        public string Alphabet;
        public SkeletonData Egg;
        public string TextAnswer;
        public Sprite ImageAnswer;
        public AudioClip AudioWord;
    }
}
