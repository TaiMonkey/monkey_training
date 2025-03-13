using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.CF
{
    [CreateAssetMenu(fileName = "CFMookData", menuName = "CF/MookData")]
    public class CFMookData : ScriptableObject
    {
        public CFGamePlayData dataMook;
    }
}