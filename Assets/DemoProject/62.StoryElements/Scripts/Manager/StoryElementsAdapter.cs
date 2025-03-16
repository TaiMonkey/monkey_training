using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.StoryElement
{
    public class StoryElementsAdapter : Adapter
    {
        private DataConfig gameData;
        [SerializeField] private MockData mockData;

        public override T GetData<T>(int turn)
        {
            T data;

            List<ConfigAnswer> dataPlay = gameData.MockData;
            Type typeData = typeof(T);

            data = ConvertToType<T>(null);

            return data;
        }

        public override int GetMaxTurn()
        {
            return 0;
        }

        public override void SetData<T>(T data)
        {
            gameData = mockData.DataConfig;
        }
    }
}
