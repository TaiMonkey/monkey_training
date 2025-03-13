using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    [CreateAssetMenu(fileName = "MTCConfigSO", menuName = "ScriptableObjects/MTCConfig/Setting", order = 1)]
    public class MTCConfigSO : ScriptableObject
    {
        public MTCGuidingConfig mtcGuidingConfig;
    }
    [Serializable]
    public class MTCGuidingConfig
    {
        public AudioClip sfxAppear;
        public AudioClip sfxClick;
        public AudioClip sfxUnClick;
        public int timeDelayStart;
        public int timeDelay;
    }
}