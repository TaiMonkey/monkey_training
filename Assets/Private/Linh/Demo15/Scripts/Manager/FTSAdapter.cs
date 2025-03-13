using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.FTS
{


    public class FTSAdapter : Adapter
    {
        private FTSDataConfig gameData;
        [SerializeField] private FTSMookData mookData; 
           public override T GetData<T>(int turn)
            {
                T data;
                Turn turnData = gameData.ListTurn[turn];

                Type typeData = typeof(T);
                if (typeData == typeof(FTSInitStateData))
                {
                    FTSInitStateData initStateData = new FTSInitStateData();
                    initStateData.ListFishAnsDatas = GetListCaAnsData(turnData.ListAnswerButton);
                    data = ConvertToType<T>(initStateData);
                }
               
                else
                {
                    data = ConvertToType<T>(null);
                }
                return data;

            }

            private List<FishnAnswerData> GetListCaAnsData(List<AnswerButton> ListFishAnswerButton)
            {
                List<FishnAnswerData> listFishAnswerDatas = new List<FishnAnswerData>();
                for (int count = 0; count < ListFishAnswerButton.Count; count++)
                {
                    AnswerButton answerButton = ListFishAnswerButton[count];

                    FishnAnswerData buttonfishAnswerData = new FishnAnswerData();
                    buttonfishAnswerData.Data = answerButton.Text;
                    buttonfishAnswerData.audioClip = answerButton.AnswerAudio;
                    buttonfishAnswerData.IsCorect = answerButton.IsCorrect;

                listFishAnswerDatas.Add(buttonfishAnswerData);
                }
                return listFishAnswerDatas;
            }

            public override int GetMaxTurn()
            {
                return gameData.ListTurn.Count;
            }

            public override void SetData<T>(T data)
            {
                gameData = mookData.fTSDataConfig;
            }
        
    }
}
