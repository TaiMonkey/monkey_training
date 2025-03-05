using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.GameTest
{
    public class GameTestDependency : Dependency
    {
        [SerializeField] private List<AnimalButtonController> buttonBearControllers;
        [SerializeField] private List<AnimalButtonController> buttonTigerControllers;
        [SerializeField] private List<GameObject> bearSnapPoints;
        [SerializeField] private List<GameObject> tigerSnapPoints;
        [SerializeField] private CageController cageTiger;
        [SerializeField] private CageController cageBear;
        [SerializeField] private GameTestConfig gameTestConfig;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform parentOrigin;
        [SerializeField] private Transform bearParentSnapPoint;
        [SerializeField] private Transform tigerParentSnapPoint;
        //guiding
        [SerializeField] private Image handLong;
        [SerializeField] private Transform animal;
        [SerializeField] private CanvasGroup canvasGroup;


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
                initStateDependency.Canvas = canvas;
                initStateDependency.ParentOrigin = parentOrigin;
                initStateDependency.BearParentSnapPoint = bearParentSnapPoint;
                initStateDependency.TigerParentSnapPoint = tigerParentSnapPoint;

                data = ConvertToType<T>(initStateDependency);
            }
            else if (typeData == typeof(GameTestGamePlayStateDependency))
            {
                GameTestGamePlayStateDependency gamePlayDependency = new GameTestGamePlayStateDependency();
                gamePlayDependency.AnimalButtonConfig = gameTestConfig.AnimalButton;
                gamePlayDependency.AnimConfig = gameTestConfig.AnimConfig;
                gamePlayDependency.TigerButtonControllers = buttonTigerControllers;
                gamePlayDependency.BearButtonControllers = buttonBearControllers;
                gamePlayDependency.BearSnapPoints = bearSnapPoints;
                gamePlayDependency.TigerSnapPoints = tigerSnapPoints;

                data = ConvertToType<T>(gamePlayDependency);
            }
            else if (typeData == typeof(GameTestGuidingStateDependency))
            {
                GameTestGuidingStateDependency guidingDependency = new GameTestGuidingStateDependency();
                guidingDependency.HandLong = handLong;
                guidingDependency.ButtonBearControllers = buttonBearControllers;
                guidingDependency.ButtonTigerControllers = buttonTigerControllers;
                guidingDependency.Animal = animal;
                guidingDependency.UiGuiding = canvasGroup;
                guidingDependency.AnimalButtonConfig = gameTestConfig.AnimalButton;

                data = ConvertToType<T>(guidingDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}
