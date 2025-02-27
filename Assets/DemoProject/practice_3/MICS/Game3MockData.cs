using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game3Demo
{
    [CreateAssetMenu(fileName = "Game3Demo", menuName = "ScriptableObjects/Game3Demo/MockData", order = 1)]
    public class Game3MockData : ScriptableObject
    {
        public Game3DataConfig Game3DataConfig;
        public AudioClip BG_audioClip;
    }
}