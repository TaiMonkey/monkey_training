using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoAdapter1 : Adapter
{
    [SerializeField] private DemoMockData demoMockData;
    [SerializeField] private bool isMockData;
    private DemoGamePlayData1 demoGamePlayData1;

    public override T GetData<T>(int turn)
    {
        T data;
        Type listType = typeof(T);
        if(listType == typeof(DemoGamePlayData1))
        {
            data = ConvertToType<T>(demoGamePlayData1);
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
        if(isMockData)
        {
            demoGamePlayData1 = new DemoGamePlayData1();
            demoGamePlayData1 = demoMockData.mockDataGamePlay;
        }
    }
}

[Serializable]
public class DemoData
{
    public List<Sprite> sprites;
    public List<string> texts;
}

[Serializable]
public class DemoGamePlayData1
{
    public DemoData demoData;
}
