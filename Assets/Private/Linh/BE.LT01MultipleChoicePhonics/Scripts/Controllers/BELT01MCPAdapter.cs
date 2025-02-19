using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BELT01MCPAdapter : Adapter
{
    [SerializeField] private MookData mookData;
    [SerializeField] private bool isMookData;
    private BELT01GamePlayData gamePlayData;
    private void Start()
    {
        SetData(gamePlayData);
    }
    public override T GetData<T>(int turn)
    {
        T data;

        Type listType = typeof(T);
        if (listType == typeof(BELT01MCPInitStateData))
        {
            BELT01MCPInitStateData bELT01MCPInitStateData = new BELT01MCPInitStateData();
            bELT01MCPInitStateData.CurrentTurn = gamePlayData.listTurn[turn];
            data = ConvertToType<T>(bELT01MCPInitStateData);
        }
        else
            data = ConvertToType<T>(null);
        return data; 
    }
    /* if (isMookData)
     {
         gamePlayData = new BELT01GamePlayData();
         gamePlayData = mookData.dataMook;
     }
     Type listType = typeof(T);
     if(listType == typeof(BELT01Turn))
     {
         data = ConvertToType<T>(gamePlayData.listTurn[turn]);
     }
     else if (listType == typeof(BELT01GamePlayData))
     {
         data = ConvertToType<T>(gamePlayData);
     }
     else if(listType == typeof(BELT01MCPInitStateData))
     {
         BELT01MCPInitStateData bELT01MCPInitStateData = new BELT01MCPInitStateData();
         bELT01MCPInitStateData.CurrentTurn = gamePlayData.listTurn[turn];
         data = ConvertToType<T>(gamePlayData);
     }
     else
     {
         data = ConvertToType<T>(null);
      }
    */
      
    

    public override int GetMaxTurn()
    {
        throw new System.NotImplementedException();
    }

    public override void SetData<T>(T data)
    {
        if (isMookData)
        {
            gamePlayData = new BELT01GamePlayData();
            gamePlayData = mookData.dataMook;
        }
    }

     

   
}
  [Serializable]
    public class BELT01GamePlayData
    {
        public List<BELT01Turn> listTurn;
    }

    [Serializable]
    public class BELT01Turn
    {
        public List<BELT01ButtonData> listButton;
    }

    [Serializable]
    public class BELT01ButtonData
    {
        public string text;
        public AudioClip audioClip;
        public bool iscorrect;
    } 