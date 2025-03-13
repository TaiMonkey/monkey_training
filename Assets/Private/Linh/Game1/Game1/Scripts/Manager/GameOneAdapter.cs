using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneAdapter : Adapter
    {
        private Game1DataConfig gameData;
        [SerializeField] private Game1MookData mookData;
        public override T GetData<T>(int turn)
        {
            T data;
            Turn turnData = gameData.ListTurn[turn];

            Type typeData = typeof(T);
            if (typeData == typeof(GameOneInitStateData))
            {
                GameOneInitStateData initStateData = new GameOneInitStateData();
                initStateData.DataQuestion = turnData.Question;
                initStateData.AudioClipQuesion = turnData.QuestionAudio;
                initStateData.ListButtonAnswerDatas = GetListButtonAnswerData(turnData.ListAnswerButton);
                data = ConvertToType<T>(initStateData);
            }
            else if (typeData == typeof(GameOneIntroStateData))
            {
                GameOneIntroStateData introStateData = new GameOneIntroStateData();
                introStateData.AudioClip = gameData.AudioClipIntro;
                data = ConvertToType<T>(introStateData);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;

        }

        private List<ButtonAnswerData> GetListButtonAnswerData(List<AnswerButton> ListAnswerButton)
        {
            List<ButtonAnswerData> listButtonAnswerDatas = new List<ButtonAnswerData>();
            for(int count = 0; count < ListAnswerButton.Count; count++)
            {
                AnswerButton answerButton = ListAnswerButton[count];

                ButtonAnswerData buttonAnswerData = new ButtonAnswerData();
                buttonAnswerData.Data = answerButton.Text;
                buttonAnswerData.audioClip = answerButton.AnswerAudio;
                buttonAnswerData.IsCorect = answerButton.IsCorrect;

                listButtonAnswerDatas.Add(buttonAnswerData);
            }
            return listButtonAnswerDatas;
        }

        public override int GetMaxTurn()
        {
            return gameData.ListTurn.Count;
        }

        public override void SetData<T>(T data)
        {
            gameData = mookData.Game1DataConfig;
        }
    }
}
