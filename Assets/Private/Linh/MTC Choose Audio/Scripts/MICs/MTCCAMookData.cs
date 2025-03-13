using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    [CreateAssetMenu(fileName = "MTCCA", menuName = "ScriptableObjects/GameMTCCA/MookData", order = 1)]
    public class MTCCAMookData : ScriptableObject
    {
         public MTCCADataConfig MTCCADataConfig;
    }
}