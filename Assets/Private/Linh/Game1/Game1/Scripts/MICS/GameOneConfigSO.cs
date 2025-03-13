using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    [CreateAssetMenu(fileName = "GameOneConfigSO", menuName = "ScriptableObjects/GameOneCFSO/Setting", order = 1)]
    public class GameOneConfigSO : ScriptableObject
    {
        public GameOneGuidingConfig gameOneGuidingConfig;
    }
    [Serializable]
    public class GameOneGuidingConfig
    {
        public AudioClip sfxAppear;
        public AudioClip sfxClick;
        public AudioClip sfxUnClick;
        public int timeDelayStart;
        public int timeDelay;
    }
}