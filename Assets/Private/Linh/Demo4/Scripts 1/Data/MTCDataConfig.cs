using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTC
{
    [Serializable]
    public class MTCDataConfig
    {
        public List<Turn> ListTurn;
        public AudioClip AudioClipIntro;
    }


    [Serializable]
    public class Turn
    {
        public string Question;
        public AudioClip QuestionAudio;

        public List<AnswerButton> ListAnsButton;
    }

    [Serializable]
    public class AnswerButton
    {
        public Sprite imageAns;
        public AudioClip AnswerAudio;
        public bool IsCorrect;
    }
}
