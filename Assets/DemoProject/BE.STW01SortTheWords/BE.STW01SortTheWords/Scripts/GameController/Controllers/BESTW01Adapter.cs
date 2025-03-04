using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Adapter : Adapter
    {

        /*[SerializeField] private BESTW01MookData mookDataSO;
        [SerializeField] private bool isMockData;*/
        private BESTW01GamePlayData gamePlayData;

        public override T GetData<T>(int turn)
        {
            T data;
           /* if (isMockData)
            {
                gamePlayData = new BESTW01GamePlayData();
                gamePlayData = mookDataSO.mookData;
            }*/

            Type listType = typeof(T);
            if (listType == typeof(BESTW01InitStateData))
            {
                BESTW01InitStateData initStateData = new BESTW01InitStateData();
                initStateData.DataPlay = gamePlayData;
                data = ConvertToType<T>(initStateData);
            }
            else if(listType == typeof(BESTW01GamePlayData))
            {
                data = ConvertToType<T>(gamePlayData);
            }
            else if (listType == typeof(List<UserEndGameData.Word>))
            {
                List<UserEndGameData.Word> wordList = new List<UserEndGameData.Word>();
                wordList.Add(gamePlayData.cardDataLeft.wordDataCard);
                wordList.Add(gamePlayData.cardDataRight.wordDataCard);
                data = ConvertToType<T>(wordList);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
        public override int GetMaxTurn()
        {
            return 0;
        }

        public override void SetData<T>(T data)
        {
            DataGameModel dataGameModel = ConvertToType<DataGameModel>(data);
            if (dataGameModel == null) LogMe.Log("Lucanhtai dataGameModel BESTW01SortTheWords == null");
            LogMe.Log(dataGameModel.JsonConfig);
            BESTW01ConfigGameData configDataGame = JsonUtility.FromJson<BESTW01ConfigGameData>(dataGameModel.JsonConfig);
            if (configDataGame == null) LogMe.Log("Lucanhtai configDataGame BESTW01SortTheWords == null");

            List<BESTW01ModelData> listDataTurn = configDataGame.data.ToList();
            listDataTurn = listDataTurn.OrderBy(item => item.order).ToList();

            BESTW01GamePlayData playData = new BESTW01GamePlayData();
            playData.cardDataLeft = new BESTW01CardData();
            playData.cardDataRight = new BESTW01CardData();

            List<BESTW01CardData> listCard = new List<BESTW01CardData>();
            listCard.Add(playData.cardDataLeft);
            listCard.Add(playData.cardDataRight);

            for (int i = 0; i < listDataTurn[0].question_data.Length; i++)
            {
                int idQuestion = listDataTurn[0].question_data[i];
                listCard[i].wordDataCard = new UserEndGameData.Word();
                listCard[i].wordId = idQuestion;
                listCard[i].text = dataGameModel.DataGamePrimitiveDict[idQuestion].Text;
                listCard[i].audio = dataGameModel.DataGamePrimitiveDict[idQuestion].AudioDataGameModelsList[0].AudioClip;
                listCard[i].sprite = dataGameModel.DataGamePrimitiveDict[idQuestion].ImageDataGameModelsList[0].Sprite;
                listCard[i].typeBox = (i == 0) ?  BESTW01TypeBox.Left : BESTW01TypeBox.Right;
                listCard[i].wordDataCard.TextID = dataGameModel.DataGamePrimitiveDict[idQuestion].TextID;
                listCard[i].wordDataCard.Type = dataGameModel.DataGamePrimitiveDict[idQuestion].Type;
            }
            playData.cardDataLeft = listCard[0];
            playData.cardDataRight = listCard[1];

            gamePlayData = playData;
        }
    }
    //Data play
    [Serializable]
    public class BESTW01GamePlayData
    {
        public BESTW01CardData cardDataLeft;
        public BESTW01CardData cardDataRight;
    }

    [Serializable]
    public class BESTW01CardData
    {
        public UserEndGameData.Word wordDataCard;
        public int wordId;
        public BESTW01TypeBox typeBox;
        public string text;
        public AudioClip audio;
        public Sprite sprite;
    }

    //Data server
    [Serializable]
    public class BESTW01ConfigGameData
    {
        public BESTW01ModelData[] data;
    }

    [Serializable]
    public class BESTW01ModelData
    {
        public int order;
        public int[] question_data;
    }
}