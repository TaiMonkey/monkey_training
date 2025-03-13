using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.FTS
{
    [Serializable]
    public class FTSDataConfig
    {
        public List<Turn> ListTurn;
        public AudioClip AudioClipIntro;
    }


    [Serializable]
    public class Turn
    {
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
