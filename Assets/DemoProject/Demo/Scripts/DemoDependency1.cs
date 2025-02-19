using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoDependency1 : Dependency
{
    [SerializeField] private List<Image> lists_prite;

    public override T GetStateData<T>()
    {
        T data;
        Type listType = typeof(T);
        if(listType == typeof(DemoInitStateObjectDependency))
        {
            DemoInitStateObjectDependency initData = new DemoInitStateObjectDependency();
            initData.sprites = lists_prite;

            data = ConvertToType<T>(initData);
        }
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }
}
