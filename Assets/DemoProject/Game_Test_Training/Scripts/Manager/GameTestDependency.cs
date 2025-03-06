using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
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
        [SerializeField] private Transform parentOrigin;
        [SerializeField] private Transform bearParentSnapPoint;
        [SerializeField] private Transform tigerParentSnapPoint;
        //guiding
        [SerializeField] private Image handLong;
        [SerializeField] private Transform animal;
        [SerializeField] private CanvasGroup canvasGroup;
        // EndGame
        [SerializeField] private SkeletonGraphic animStar;


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
            else if (typeData == typeof(GameTestEndGameStateDependency))
            {
                GameTestEndGameStateDependency endGameDependency = new GameTestEndGameStateDependency();
                endGameDependency.ButtonBearControllers = buttonBearControllers;
                endGameDependency.ButtonTigerControllers = buttonTigerControllers;
                endGameDependency.EndGameConfig = gameTestConfig.EndGameConfig;
                endGameDependency.GameTestAnimalConfig = gameTestConfig.AnimConfig;
                endGameDependency.AnimStart = animStar;

                data = ConvertToType<T>(endGameDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}
