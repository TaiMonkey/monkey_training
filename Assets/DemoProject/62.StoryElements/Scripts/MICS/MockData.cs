using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.StoryElement
{
    [CreateAssetMenu(fileName = "StoryElement", menuName = "ScriptableObjects/StoryElement/MookData", order = 1)]

    public class MockData : ScriptableObject
    {
        public DataConfig DataConfig;
    }
}