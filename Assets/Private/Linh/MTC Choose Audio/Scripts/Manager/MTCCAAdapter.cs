using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class MTCCAAdapter : Adapter
    {
        private MTCCADataConfig gameData;
        [SerializeField] private MTCCAMookData mookData;
        public override T GetData<T>(int turn)
        {
            T data;
            Turn turnData = gameData.ListTurn[turn];
            Type typeData = typeof(T);
            if (typeData == typeof(MTCCAInitStateData))
            {
                MTCCAInitStateData initStateData = new MTCCAInitStateData();
                initStateData.AudioClipQuesion = turnData.QuestionAudio;
                initStateData.ImageQuestion = turnData.ImageQuestion;
                initStateData.ListButtonAnswerDatas= GetListButtonAnswerData(turnData.ListAnswerButton);
                data = ConvertToType<T>(initStateData);
            }
            else if (typeData == typeof(MTCCAIntroStateData))
            {
                MTCCAIntroStateData introStateData = new MTCCAIntroStateData();
                introStateData.AudioClip = gameData.sfxCTA;
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
            for (int count = 0; count < ListAnswerButton.Count; count++)
            {
                AnswerButton answerButton = ListAnswerButton[count];
                ButtonAnswerData buttonAnswerData = new ButtonAnswerData();
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
            gameData = mookData.MTCCADataConfig;
        }
    }
}
