using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    [CreateAssetMenu(fileName = "BESTW01MookData", menuName = "ScriptableObjects/BESTW01SortTheWords/MookData", order = 1)]

    public class BESTW01MookData : ScriptableObject
    {
        public BESTW01GamePlayData mookData;
    }
}