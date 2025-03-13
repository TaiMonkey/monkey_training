
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    [Serializable]
    public class Game1DataConfig
    {
        public List<Turn> ListTurn;
        public AudioClip AudioClipIntro;
    }


    [Serializable]
    public class Turn
    {
        public string Question ;
        public AudioClip QuestionAudio;

        public List<AnswerButton> ListAnswerButton;
    }

    [Serializable]
    public class AnswerButton
    {
        public string Text;
        public AudioClip AnswerAudio;
        public bool IsCorrect;
    }
}
