using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Monkey.Game.StoryElement
{
    [Serializable]
    public class DataConfig
    {
        public List<ConfigAnswer> MockData;
    }

    [Serializable]
    public class ConfigAnswer
    {
        public Sprite ImageAnswer;
        public string Text;
        public int Index;
    }
}
