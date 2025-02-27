using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{
    [Serializable]
    public class Game2DataConfig
    {
        public ListTurn listTurn;
    }

    [Serializable]
    public class ListTurn
    {
        public List<Turn> turn;
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
