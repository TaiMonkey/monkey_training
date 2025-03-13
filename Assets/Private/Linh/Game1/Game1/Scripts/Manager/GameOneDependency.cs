
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
        [SerializeField] private GameOneConfigSO gameOneConfigSO;
        [SerializeField] private Transform transbuttonAnsGroup;
        [SerializeField] private CanvasGroup uiGuiding;
        [SerializeField] private Image handPoint;
        [SerializeField] private Image handHold;
        [SerializeField] private AudioClip audioCorrect;

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
            else if (typeData == typeof(GameOneGuidingDependency))
            {
                GameOneGuidingDependency guidingDependency = new GameOneGuidingDependency();
                guidingDependency.GameOneGuidingConfig = gameOneConfigSO.gameOneGuidingConfig;
                guidingDependency.ButtonAnswerControllers = buttonAnswerButtons;
                guidingDependency.TransButtonAnsGroup = transbuttonAnsGroup;
                guidingDependency.UiGuiding = uiGuiding;
                guidingDependency.HandPoint = handPoint;
                guidingDependency.HandHold = handHold;

                data = ConvertToType<T>(guidingDependency);
            }
             else if (typeData == typeof(GameOneDelayFinishDependency))
            {
                GameOneDelayFinishDependency delayFinishDependency = new GameOneDelayFinishDependency();
                delayFinishDependency.AudioClip = audioCorrect;
                data = ConvertToType<T>(delayFinishDependency);
            }
            else if (typeData == typeof(NextTurnStateDependency))
            {
                NextTurnStateDependency nextTurnStateDependency = new NextTurnStateDependency();
                nextTurnStateDependency.QuestionController = questionLabelController;
                nextTurnStateDependency.ButtonSpeakerController = buttonSpeakerController;
                nextTurnStateDependency.ButtonAnswerControllers = buttonAnswerButtons;

                data = ConvertToType<T>(nextTurnStateDependency);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}
