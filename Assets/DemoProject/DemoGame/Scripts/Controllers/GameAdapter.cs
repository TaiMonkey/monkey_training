using System;
using System.Collections.Generic;
using UnityEngine;

public class GameAdapter : Adapter
{
    private GamePlayData gamePlayData;
    [SerializeField] private MockData mockData;
    [SerializeField] private bool isMockData;

    public override T GetData<T>(int turn)
    {
        T data;
        if(isMockData)
        {
            gamePlayData = new GamePlayData();
            gamePlayData = mockData.gamePlayData;
        }
        Type listType = typeof(T);
        if (listType == typeof(DemoButton))
        {
            DemoButton demoButton = new DemoButton();
            demoButton.gamePlayData = gamePlayData;
            data = ConvertToType<T>(gamePlayData);
        }
        else
        {
            data = ConvertToType<T>(null);
        }

        return data;
    }

    public override int GetMaxTurn()
    {
        throw new System.NotImplementedException();
    }

    public override void SetData<T>(T data)
    {
        throw new System.NotImplementedException();
    }

}

[Serializable]
public class GamePlayData
{
    public List<Turn> listTurn;
}

[Serializable]
public class Turn
{
    public List<ButtonData> listDataButton;
}

[Serializable]
public class ButtonData
{
    public string text;
    public AudioClip audio;
    public bool isCorrect;
}
