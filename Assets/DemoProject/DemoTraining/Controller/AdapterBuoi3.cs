using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTrainingDemo
{
    public class AdapterBuoi3 : Adapter
    {
        [SerializeField] private MookDataBuoi3 mookData;
        [SerializeField] private bool isMookData;
        private DemoGamePlayData gamePlayData;

        public override T GetData<T>(int turn)
        {
            T data;

            Type listType = typeof(T);
            if (listType == typeof(DemoGamePlayData))
            {
                data = ConvertToType<T>(gamePlayData);
            }
            else if (listType == typeof(DemoTurn))
            {
                data = ConvertToType<T>(gamePlayData.listTurn[turn]);
            }
            else
                data = ConvertToType<T>(null);

            Debug.LogError("2" + gamePlayData.listTurn[0].listDataButton[0].text);
            return data;
        }

        public override int GetMaxTurn()
        {
            return gamePlayData.listTurn.Count;
        }

        public override void SetData<T>(T data)
        {
            if (isMookData)
            {
                gamePlayData = new DemoGamePlayData();
                gamePlayData = mookData.dataMook;
                Debug.LogError("1" + gamePlayData.listTurn[0].listDataButton[0].text);
            }
        }
    }

    [Serializable]
    public class DemoGamePlayData
    {
        public List<DemoTurn> listTurn;
    }


    [Serializable]
    public class DemoTurn
    {
        public List<DemoButtonData> listDataButton;
    }

    [Serializable]
    public class DemoButtonData
    {
        public string text;
        public AudioClip audio;
        public bool isCorrect;
    }
}

