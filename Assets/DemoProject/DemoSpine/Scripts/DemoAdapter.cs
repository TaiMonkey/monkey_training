using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoAdapter : Adapter
{
    [SerializeField] private DeMockData mock_data;
    [SerializeField] private bool isMockData;
    private GamePlayData1 gamePlayData;

    public override T GetData<T>(int turn)
    {
        T data;
        if (isMockData)
        {
            gamePlayData = new GamePlayData1();
            gamePlayData = mock_data.dataMock;
        }

        Type listType = typeof(T);
        if(listType == typeof(InitState))
        {
            InitStateData initStateData = new InitStateData();
            initStateData.listData = gamePlayData;
            data = ConvertToType<T>(initStateData);
        }
        else
        {
            data = ConvertToType<T>(null);
        }

        return data;
            
    }

    public override int GetMaxTurn()
    {
        return gamePlayData.listTurn.Count;
    }

    public override void SetData<T>(T data)
    {
        
    }
}

[Serializable]
public class GamePlayData1
{
    public List<DemoTurn> listTurn;
}

[Serializable]
public class DemoTurn
{
    public List<DemoButton1> buttons;
}

[Serializable]
public class DemoButton1
{
    public string textButton;
    public AudioClip audio;
    public bool is_correct;
}
