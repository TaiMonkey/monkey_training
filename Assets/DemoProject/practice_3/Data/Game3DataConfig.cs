using System.Collections.Generic;
using UnityEngine;
using System;

namespace Monkey.Game.Game3Demo
{
    [Serializable]
    public class Game3DataConfig
    {
        public List<Turn> turns;
    }

    [Serializable]
    public class Turn
    {
        public List<ImageButton> sprites;
        public List<TextButton> dataTexs;
    }

    [Serializable]
    public class TextButton
    {
        public string textAns;
        public int index;
    }

    [Serializable]
    public class ImageButton
    {
        public Sprite spriteQues;
        public int index;
    }
}