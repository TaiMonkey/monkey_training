using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game2Demo
{

    public class Game2Adapter : Adapter
    {
        private Game2DataConfig gameData;
        [SerializeField] private Game2MockData mockData;

        public override T GetData<T>(int turn)
        {
            T data;
            Type typeData = typeof(T);
            Turn turnData = gameData.turn;

            if (typeData == typeof(Game2InitStateData))
            {
                Game2InitStateData initStateData = new Game2InitStateData();
                initStateData.DataQuestion = turnData.TextQuestion;
                initStateData.AudioClipQuestion = turnData.QuestionAudio;
                initStateData.buttonAnswerDatas = GetButtonAnswerDatas(turnData.ListAnswerButtons);

                data = ConvertToType<T>(initStateData);
            }
            else if(typeData == typeof(Game2IntroStateData))
            {
                Game2IntroStateData introStateData = new Game2IntroStateData();
                introStateData.AudioClip = turnData.QuestionAudio;

                data = ConvertToType<T>(introStateData);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;

        }

        private List<ButtonAnswerData> GetButtonAnswerDatas(List<AnswerButton> List)
        {
            List<ButtonAnswerData> Lists = new List<ButtonAnswerData>();
            for(int i = 0; i < List.Count; i ++)
            {
                AnswerButton answerButton = List[i];
                ButtonAnswerData buttonAnswerData = new ButtonAnswerData();
                buttonAnswerData.audioClip = answerButton.AnswerAudio;
                buttonAnswerData.Data = answerButton.Image;
                buttonAnswerData.IsCorrect = answerButton.Iscorrect;

                Lists.Add(buttonAnswerData);
            }
            return Lists;
        }

        public override int GetMaxTurn()
        {
            return 1;
        }

        public override void SetData<T>(T data)
        {
            gameData = mockData.Game2DataConfig;
        }
    }
}