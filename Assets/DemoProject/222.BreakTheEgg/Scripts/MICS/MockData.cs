using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    [CreateAssetMenu(fileName = "BreakTheEgg", menuName = "ScriptableObjects/BreakTheEgg/MookData", order = 1)]
    public class MockData : ScriptableObject
    {
        public DataConfig DataConfig;
    }
}
