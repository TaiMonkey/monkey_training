using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    [CreateAssetMenu(fileName = "MTCCAConfigSO", menuName = "ScriptableObjects/MTCChooseAudio/Setting", order = 1)]
    public class MTCCAConfigSO : ScriptableObject
    {
        public MTCCAIntroConfig introConfig;
        public MTCCAPlayConfig playConfig;
        public MTCCAGuidingConfig guidingConfig;
        public MTCCEndGameConfig endGameConfig;
        public Anim anim;
    }
    [Serializable]
    public class MTCCAIntroConfig
    {
        public AudioClip sfxPopup;
        public int timeDelay;
    }
    [Serializable]
    public class MTCCAPlayConfig
    {
        public AudioClip sfxClick;
        public AudioClip sfxCorrect;
        public AudioClip sfxWrong;
        public AudioClip sfxTiaset;
        public AudioClip sfxGhepbophan;
    }
    [Serializable]
    public class MTCCAGuidingConfig
    {
        public AudioClip sfxHintAppear;
        public AudioClip sfxClick;
        public AudioClip sfxUnclick;
        public int timeDelayStart;
        public int timeDelay;
    }
    [Serializable]
    public class MTCCEndGameConfig
    {
        public AudioClip sfxMaxlenPlane;
        public AudioClip sfxPlaneFly;
    }
    [Serializable]
    public class Anim
    {
        public string startingAnim1;
        public string startingAnim2;
        public string startingAnim3;
        public string startingAnim4;
        public string startingAnim5;
        public string startingAnim6;
        public string startingAnim7;
        public string startingAnim8;
    }
}