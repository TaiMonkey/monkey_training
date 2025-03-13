using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.CF
{
    public class CFAdapter : Adapter
    {
        [SerializeField] private CFMookData mookData;
        [SerializeField] private bool isMookData;
        private CFGamePlayData gamePlayData;
        private void Start()
        {
            SetData(gamePlayData);
        }
        public override T GetData<T>(int turn)
        {
            T data;

            Type listType = typeof(T);
            if (listType == typeof(CFInitStateData))
            {
                CFInitStateData cFInitStateData = new CFInitStateData();
                cFInitStateData.CurrentTurn = gamePlayData.listTurn[turn];
                data = ConvertToType<T>(cFInitStateData);
            }
            else if (listType == typeof(CFIntroStateData))
            {
                CFIntroStateData introStateData = new CFIntroStateData();
                introStateData.AudioClip = gamePlayData.AudioClipIntro;
                data = ConvertToType<T>(introStateData);
            }
            else
                data = ConvertToType<T>(null);
            return data;
        }

        public override void SetData<T>(T data)
        {
            if (isMookData)
            {
                gamePlayData = new CFGamePlayData();
                gamePlayData = mookData.dataMook;
            }
        }

        public override int GetMaxTurn()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class CFGamePlayData
    {
        public List<CFTurn> listTurn;
        public AudioClip AudioClipIntro;
    }

    [Serializable]
    public class CFTurn
    {
        public List<CFButtonJarData> listButtonJar;
        public List<CFButtonDomDomData> listButtonDomdom;

    }

    [Serializable]
    public class CFButtonJarData
    {
        public Sprite imange;
        public AudioClip audioClip;
        public int correctID;
    }
    [Serializable]
    public class CFButtonDomDomData
    {
        public string text;
        public AudioClip audioClip;
        public int correctID;

    }
}