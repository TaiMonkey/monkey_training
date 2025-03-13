using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    [Serializable]
    public class MTCCADataConfig
    {
        public List<Turn> ListTurn;
        public AudioClip sfxCTA;
    }


    [Serializable]
    public class Turn
    {
        public AudioClip QuestionAudio;
        public Sprite ImageQuestion;
        public List<AnswerButton> ListAnswerButton;
    }

    [Serializable]
    public class AnswerButton
    {
        public AudioClip AnswerAudio;
        public bool IsCorrect;
    }
  
    
}
