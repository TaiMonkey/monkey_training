using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.BreakTheEgg
{
    public class BreakTheEggDependency : Dependency
    {
        [SerializeField] private List<ButtonEggController> buttonEggController;
        [SerializeField] private LogoMovement logoMovement;
        [SerializeField] private ConfigDataState configDataState;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private List<Image> imageBack;
        [SerializeField] private List<Image> imageFront;
        [SerializeField] private List<Image> ImageAnswerEggs;
        [SerializeField] private Transform targetPointImage;

        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if (typeData == typeof(InitStateDependency))
            {
                InitStateDependency initStateDependency = new InitStateDependency();
                initStateDependency.buttonEggControllers = buttonEggController;
                initStateDependency.InitConfig = configDataState.initConfig;
                initStateDependency.SkinConfig = configDataState.skinConfig;
                initStateDependency.LogoMovement = logoMovement;
                initStateDependency.ListImageBack = imageBack;
                initStateDependency.ListImageFront = imageFront;
                initStateDependency.ListImageAnswer = ImageAnswerEggs;

                data = ConvertToType<T>(initStateDependency);
            }
            else if(typeData == typeof(IntroStateDependency))
            {
                IntroStateDependency introStateDependency = new IntroStateDependency();
                introStateDependency.ButtonEggControllers = buttonEggController;
                introStateDependency.IntroConfig = configDataState.introConfig;
                introStateDependency.TargetPoint = targetPoint;
                introStateDependency.ListImageBack = imageBack;
                introStateDependency.ListImageFront = imageFront;

                data = ConvertToType<T>(introStateDependency);
            }
            else if (typeData == typeof(GameplayStateDependency))
            {
                GameplayStateDependency gameplayStateDependency = new GameplayStateDependency();
                gameplayStateDependency.TargetPoint = targetPoint;
                gameplayStateDependency.SkinConfig = configDataState.skinConfig;
                gameplayStateDependency.GameplayConfig = configDataState.gameplayConfig;

                data = ConvertToType<T>(gameplayStateDependency);
            }
            else if (typeData == typeof(ResultKnockEggStateDependency))
            {
                ResultKnockEggStateDependency knockEggStateDependency = new ResultKnockEggStateDependency();
                knockEggStateDependency.TargetPoint = targetPoint;
                knockEggStateDependency.ButtonEggControllers = buttonEggController;
                knockEggStateDependency.GameplayConfig = configDataState.gameplayConfig;

                data = ConvertToType<T>(knockEggStateDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }

}
