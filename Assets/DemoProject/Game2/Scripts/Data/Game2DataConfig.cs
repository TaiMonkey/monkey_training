using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    [Serializable]
    public class Game2DataConfig
    {
        public Turn turn;
    }

    [Serializable]
    public class Turn
    {
        public AudioClip QuestionAudio;
        public string TextQuestion;
        public List<AnswerButton> ListAnswerButtons;
    }

    [Serializable]
    public class AnswerButton
    {
        public Sprite Image;
        public AudioClip AnswerAudio;
        public bool Iscorrect;
    }
}
