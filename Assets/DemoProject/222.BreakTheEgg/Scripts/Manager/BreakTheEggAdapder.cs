using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.BreakTheEgg
{
    public class BreakTheEggAdapder : Adapter
    {
        private DataConfig gameData;
        [SerializeField] private MockData mockData;

        public override T GetData<T>(int turn)
        {
            T data;
            List<ConfigEgg> dataPlay = gameData.ConfigEgg;

            Type typeData = typeof(T);
            if (typeData == typeof(InitStateData))
            {
                InitStateData initStateData = new InitStateData();
                initStateData.ListButtonEggData = GetListButtonEggData(dataPlay);

                data = ConvertToType<T>(initStateData);
            }
            else
            {
                data = ConvertToType<T>(null);
            }

            return data;
        }

        private List<ButtonEggData> GetListButtonEggData(List<ConfigEgg> listData)
        {
            List<ButtonEggData> listEggData = new List<ButtonEggData>();
            for(int i = 0; i < listData.Count; i ++)
            {
                ConfigEgg dataEgg = listData[i];

                ButtonEggData buttonEggData = new ButtonEggData();
                buttonEggData.AlphaBetAnser = dataEgg.Alphabet;
                buttonEggData.TextAnswer = dataEgg.TextAnswer;
                buttonEggData.ImageAnswer = dataEgg.ImageAnswer;

                listEggData.Add(buttonEggData);
            }

            return listEggData;
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
