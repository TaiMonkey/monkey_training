using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFDependency : Dependency
{
    [SerializeField] private List<ButtonJarController> listButtonJar;
    [SerializeField] private List<ButtonDomdomCotroller> listButtonDomDom;
    public override T GetStateData<T>()
    {
        T data;
        Type typeData = typeof(T);
        if (typeData == typeof(CFInitStateObjectDependency))
        {
            CFInitStateObjectDependency cFInitStateObjectDependency = new CFInitStateObjectDependency();
            cFInitStateObjectDependency.ButtonsDomdom = listButtonDomDom;
            cFInitStateObjectDependency.ButtonsJar = listButtonJar;
            data = ConvertToType<T>(cFInitStateObjectDependency);
        }
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }

}
    
   
