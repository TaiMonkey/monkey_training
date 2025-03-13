using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTC
{
    public class MTCAdapter : Adapter
    {
        private MTCDataConfig gameData;
        [SerializeField] private MTCMookData mookData;
        public override T GetData<T>(int turn)
        {
            T data;
            Turn turnData = gameData.ListTurn[turn];
            Type typeData = typeof(T);
            if (typeData == typeof(MTCInitStateData))
            {
                MTCInitStateData initStateData = new MTCInitStateData();
                initStateData.DataQuestion = turnData.Question;
                initStateData.AudioQuestion = turnData.QuestionAudio;
                initStateData.ListButtonsAnsData = GetListButtonAnsDatas(turnData.ListAnsButton);

                data = ConvertToType<T>(initStateData);
            }
            else if (typeData == typeof(MTCIntroStateData))
            {
                MTCIntroStateData introStateData = new MTCIntroStateData();
                introStateData.AudioClip = gameData.AudioClipIntro;
                data = ConvertToType<T>(introStateData);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;

        }
        private List<ButtonAnsData> GetListButtonAnsDatas(List<AnswerButton> listAnsButton)
        {
            List<ButtonAnsData> listButtonAnsData = new List<ButtonAnsData>();
            for(int count = 0; count < listAnsButton.Count; count++)
            {
                AnswerButton answerButton = listAnsButton[count];
                ButtonAnsData buttonAnsData = new ButtonAnsData();
                buttonAnsData.Data = answerButton.imageAns;
                buttonAnsData.audioClip = answerButton.AnswerAudio;
                buttonAnsData.IsCorrect = answerButton.IsCorrect;

                listButtonAnsData.Add(buttonAnsData);
            }
            return listButtonAnsData;
        }

        public override int GetMaxTurn()
        {
            Debug.LogError(gameData.ListTurn.Count.ToString());
            return gameData.ListTurn.Count;
        }

        public override void SetData<T>(T data)
        {
            gameData = mookData.mtcDataConfig;
        }

    }
}
