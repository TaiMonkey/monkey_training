using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    [CreateAssetMenu(fileName = "MTCMookData", menuName = "ScriptableObjects/GameMTC/MookData", order = 1)]
    public class MTCMookData : ScriptableObject
    {
        public MTCDataConfig mtcDataConfig;
    }
}
