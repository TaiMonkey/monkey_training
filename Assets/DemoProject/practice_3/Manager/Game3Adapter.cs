using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game3Demo
{
    public class Game3Adapter : Adapter
    {
        private Game3DataConfig gameData;
        [SerializeField] private Game3MockData mockData;

        public override T GetData<T>(int turn)
        {
            T data;
            Type typeData = typeof(T);
            Turn turnData = gameData.turns[turn];


            data = ConvertToType<T>(null);
            return data;
        }

        public override int GetMaxTurn()
        {
            return gameData.turns.Count;
        }

        public override void SetData<T>(T data)
        {
            gameData = mockData.Game3DataConfig;
        }
    }
}
