using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.CF
{
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
            else if (typeData == typeof(CFIntroStateDependency))
            {
                CFIntroStateDependency cFIntroStateDependency = new CFIntroStateDependency();
                cFIntroStateDependency.ButtonDomdomCotrollers = listButtonDomDom;
                cFIntroStateDependency.ButtonJarControllers = listButtonJar;
                data = ConvertToType<T>(cFIntroStateDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }

    }
}
    
   
