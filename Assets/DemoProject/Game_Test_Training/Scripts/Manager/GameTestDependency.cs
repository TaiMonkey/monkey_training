using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestDependency : Dependency
    {
        [SerializeField] private List<ButtonBearController> buttonBearControllers;
        [SerializeField] private List<ButtonTigerController> buttonTigerControllers;
        [SerializeField] private CageController cageTiger;
        [SerializeField] private CageController cageBear;
        [SerializeField] private GameTestConfig gameTestConfig;

        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if (typeData == typeof(GameTestInitStateDependency))
            {
                GameTestInitStateDependency initStateDependency = new GameTestInitStateDependency();
                initStateDependency.ButtonBearControllers = buttonBearControllers;
                initStateDependency.ButtonTigerControllers = buttonTigerControllers;
                initStateDependency.AudioCTA = gameTestConfig.IntroConfig.AudioCTA;
                initStateDependency.AnimConfig = gameTestConfig.AnimConfig;
                initStateDependency.Cage_Bear = cageBear;
                initStateDependency.Cage_Tiger = cageTiger;

                data = ConvertToType<T>(initStateDependency);
            }
            else if (typeData == typeof(GameTestGamePlayStateDependency))
            {
                GameTestGamePlayStateDependency gamePlayDependency = new GameTestGamePlayStateDependency();
                gamePlayDependency.AnimalButtonConfig = gameTestConfig.AnimalButton;
                gamePlayDependency.AnimConfig = gameTestConfig.AnimConfig;

                data = ConvertToType<T>(gamePlayDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}
