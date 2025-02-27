using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    [Serializable]
    public class Game4DataConfig 
    {
        public Turn turn;
    }

    [Serializable]
    public class Turn
    {
        public List<ButtonBox> ButtonBoxes;
        public List<ButtonAns> ButtonAns;
        public AudioClip AudioQuestion;
    }

    [Serializable]
    public class ButtonBox
    {
        public string QuestionText;
        public AudioClip audioChoose;
    }

    [Serializable]
    public class ButtonAns
    {
        public Sprite SpriteAns;
        public AudioClip audioClick;
    }
}
