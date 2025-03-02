using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class Game4Dependency : Dependency
    {
        [SerializeField] private List<AnswerButtonController> answerButtonController;
        [SerializeField] private List<BoxQuesController> boxQuesController;
        [SerializeField] private AnswerSpawnerController answerSpawnerController;

        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if (typeData == typeof(Game4InitStateDependency))
            {
                Game4InitStateDependency initStateDependency = new Game4InitStateDependency();
                initStateDependency.answerButtonControllers = answerButtonController;
                initStateDependency.boxQuesControllers = boxQuesController;

                data = ConvertToType<T>(initStateDependency);
            }
            else if(typeData == typeof(Game4IntroStateDependency))
            {
                Game4IntroStateDependency introStateDependency = new Game4IntroStateDependency();
                introStateDependency.AnswerButtonControllers = answerButtonController;
                introStateDependency.AnswerSpawnerController = answerSpawnerController;

                data = ConvertToType<T>(introStateDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}