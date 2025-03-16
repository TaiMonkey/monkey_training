using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.StoryElement
{
    public class StoryElementDependency : Dependency
    {
        [SerializeField] private List<ButtonAnswerController> ListButtonAnswer;
        [SerializeField] private ConfigDataState configDataState;
        [SerializeField] private List<Image> listImageBackground;

        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);
            if (typeData == typeof(InitStateDependency))
            {
                InitStateDependency initStateDependency = new InitStateDependency();
                initStateDependency.InitConfig = configDataState.InitConfig;
                initStateDependency.ListImageBackground = listImageBackground;

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
