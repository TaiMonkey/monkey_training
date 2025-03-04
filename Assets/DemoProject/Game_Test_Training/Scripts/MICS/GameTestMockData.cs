using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.GameTest
{
    [CreateAssetMenu(fileName = "GameTest", menuName = "ScriptableObjects/GameTest/MookData", order = 1)]
    public class GameTestMockData : ScriptableObject
    {
        public GameTestDataConfig GameTestDataConfig;
        public AudioClip CTA;
    }

}