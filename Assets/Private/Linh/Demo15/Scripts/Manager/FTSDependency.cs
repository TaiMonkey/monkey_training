using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Monkey.Game.FTS
{
    public class FTSDependency : Dependency
    {
        [SerializeField] private SharkController buttonSharkController;
        [SerializeField] private List<CaAnsController> buttonCaAnswerButtons;
        [SerializeField] private Transform transbuttonAnsGroup;
        [SerializeField] private CanvasGroup uiGuiding;
        

        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if (typeData == typeof(FTSInitStateDependency))
            {
                FTSInitStateDependency initStateDependency = new FTSInitStateDependency();
                initStateDependency.ButtonShark = buttonSharkController;
                initStateDependency.ListfishAns = buttonCaAnswerButtons;
                data = ConvertToType<T>(initStateDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}