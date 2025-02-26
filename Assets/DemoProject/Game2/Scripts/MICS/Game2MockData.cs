using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.Game2Demo
{
    [CreateAssetMenu(fileName = "Game2Demo", menuName = "ScriptableObjects/Game2Demo/MockData", order = 1)]
    public class Game2MockData : ScriptableObject
    {
        public Game2DataConfig Game2DataConfig;
    }
}
