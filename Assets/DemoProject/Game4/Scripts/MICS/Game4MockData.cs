using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    [CreateAssetMenu(fileName = "Game4Demo", menuName = "ScriptableObjects/Game4Demo/MookData", order = 1)]
    public class Game4MockData : ScriptableObject
    {
        public Game4DataConfig mockData;
    }
}
