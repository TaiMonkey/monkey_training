
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneDependency : Dependency
    {
        [SerializeField] private ButtonSpeakerController buttonSpeakerController;
        [SerializeField] private List<ButtonAnswerController> buttonAnswerButtons;
        [SerializeField] private QuestionLabelController questionLabelController;
        [SerializeField] private Image handLong;
        [SerializeField] private Image handShort;
        [SerializeField] private CanvasGroup uiGuiding;

        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if (typeData == typeof(GameOneInitStateDependency))
            {
                GameOneInitStateDependency gameOneInitStateDependency = new GameOneInitStateDependency();
                gameOneInitStateDependency.QuestionController = questionLabelController;
                gameOneInitStateDependency.ButtonSpeakerController = buttonSpeakerController;
                gameOneInitStateDependency.ButtonAnswerControllers = buttonAnswerButtons;

                data = ConvertToType<T>(gameOneInitStateDependency);
            }
            else if (typeData == typeof(GameOneIntroStateDependency))
            {
                GameOneIntroStateDependency introStateDependency = new GameOneIntroStateDependency();
                introStateDependency.QuestionController = questionLabelController;
                introStateDependency.ButtonSpeakerController = buttonSpeakerController;
                introStateDependency.ButtonAnswerControllers = buttonAnswerButtons;

                data = ConvertToType<T>(introStateDependency);
            }
            else if (typeData == typeof(NextTurnStateDependency))
            {
                NextTurnStateDependency nextTurnStateDependency = new NextTurnStateDependency();
                nextTurnStateDependency.QuestionController = questionLabelController;
                nextTurnStateDependency.ButtonSpeakerController = buttonSpeakerController;
                nextTurnStateDependency.ButtonAnswerControllers = buttonAnswerButtons;

                data = ConvertToType<T>(nextTurnStateDependency);
            }
            else if (typeData == typeof(GameOneGuidingStateDependency)) {
                GameOneGuidingStateDependency guidingStateDependency = new GameOneGuidingStateDependency();
                guidingStateDependency.ButtonAnswers = buttonAnswerButtons;
                guidingStateDependency.HandLong = handLong;
                guidingStateDependency.HandShort = handShort;
                guidingStateDependency.UiGuiding = uiGuiding;
                Debug.LogError(guidingStateDependency.UiGuiding + "uiGuiding");

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
