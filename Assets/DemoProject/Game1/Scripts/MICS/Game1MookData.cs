using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    [CreateAssetMenu(fileName = "GameOneDemo", menuName = "ScriptableObjects/GameOneDemo/MookData", order = 1)]
    public class Game1MookData : ScriptableObject
    {
        public Game1DataConfig Game1DataConfig;
    }
}
