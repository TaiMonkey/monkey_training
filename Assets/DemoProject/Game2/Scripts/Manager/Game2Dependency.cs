using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.Game2Demo
{
    public class Game2Dependency : Dependency
    {
        [SerializeField] private List<ButtonAnswerController> buttonAnswersController;
        [SerializeField] private ButtonSpeakerController buttonSpeakerController;
        [SerializeField] private QuestionTextController questionTextController;
        [SerializeField] private Image handLong;
        [SerializeField] private Image handShort;
        [SerializeField] private CanvasGroup uiGuiding;


        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if(typeData == typeof(Game2InitStateDependency))
            {
                Game2InitStateDependency initStateDependency = new Game2InitStateDependency();
                initStateDependency.ButtonAnswers = buttonAnswersController;
                initStateDependency.TextQuestion = questionTextController;
                initStateDependency.ButtonSpeaker = buttonSpeakerController;

                data = ConvertToType<T>(initStateDependency);
            }
            else if (typeData == typeof(Game2IntroStateDependency))
            {
                Game2IntroStateDependency introStateDependency = new Game2IntroStateDependency();
                introStateDependency.ButtonAnswers = buttonAnswersController;

                data = ConvertToType<T>(introStateDependency);
            }
            else if (typeData == typeof(Game2GuidingStateDependency))
            {
                Game2GuidingStateDependency guidingStateDependency = new Game2GuidingStateDependency();
                guidingStateDependency.ButtonAnswers = buttonAnswersController;
                guidingStateDependency.HandLong = handLong;
                guidingStateDependency.HandShort = handShort;
                guidingStateDependency.UiGuiding = uiGuiding;

                data = ConvertToType<T>(guidingStateDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }

}