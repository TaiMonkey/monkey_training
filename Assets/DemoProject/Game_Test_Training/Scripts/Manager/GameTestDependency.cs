using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestDependency : Dependency
    {
        [SerializeField] private List<AnimalButtonController> buttonBearControllers;
        [SerializeField] private List<AnimalButtonController> buttonTigerControllers;
        [SerializeField] private RectTransform cageTiger;
        [SerializeField] private RectTransform cageBear;
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
                gamePlayDependency.TigerButtonControllers = buttonTigerControllers;
                gamePlayDependency.BearButtonControllers = buttonBearControllers;

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
