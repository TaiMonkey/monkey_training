using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.FTS
{
    [CreateAssetMenu(fileName = "FTSMookData", menuName = "ScriptableObjects/FTSMookData", order = 1)]
    public class FTSMookData : ScriptableObject
    {
        public FTSDataConfig fTSDataConfig;
    }
}
