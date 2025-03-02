using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class Game4Adapter : Adapter
    {
        private Game4DataConfig gameData;
        [SerializeField] private Game4MockData mookData;

        public override T GetData<T>(int turn)
        {
            T data;
            Type typeData = typeof(T);
            Turn turnData = gameData.turn;

            if (typeData == typeof(Game4InitStateData))
            {
                Game4InitStateData initStateData = new Game4InitStateData();
                initStateData.ButtonAnswers = GetButtonAnswers(turnData.ButtonAns);
                initStateData.ButtonBoxQues = GetButtonBoxQues(turnData.ButtonBoxes);
                initStateData.AudioClip = turnData.AudioQuestion;

                data = ConvertToType<T>(initStateData);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }

        private List<ButtonAnswer> GetButtonAnswers(List<ButtonAns> buttonAns)
        {
            List<ButtonAnswer> list = new List<ButtonAnswer>();
            for (int count = 0; count < buttonAns.Count; count++)
            {
                ButtonAns buttonAnswer = buttonAns[count];
                ButtonAnswer buttonAnswerData = new ButtonAnswer();
                buttonAnswerData.AudioClip = buttonAnswer.audioClick;
                buttonAnswerData.DataAnswer = buttonAnswer.SpriteAns;

                list.Add(buttonAnswerData);
            }
                return list;
        }

        private List<ButtonBoxQues> GetButtonBoxQues(List<ButtonBox> buttonBoxQues)
        {
            List<ButtonBoxQues> list = new List<ButtonBoxQues>();
            for (int count = 0; count < buttonBoxQues.Count; count++)
            {
                ButtonBox buttonBox = buttonBoxQues[count];
                ButtonBoxQues buttonBoxData = new ButtonBoxQues();
                buttonBoxData.AudioClip = buttonBox.audioChoose;
                buttonBoxData.DataQues = buttonBox.QuestionText;

                list.Add(buttonBoxData);
            }
            return list;
        }

        public override int GetMaxTurn()
        {
            return 0;
        }

        public override void SetData<T>(T data)
        {
            gameData = mookData.mockData;
        }
    }
}