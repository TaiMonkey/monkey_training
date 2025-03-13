using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTC
{


    public class MTCDependency : Dependency
    {
        [SerializeField] private ButtonSpeakController buttonSpeakerController;
        [SerializeField] private List<ButtonAnsController> buttonAnswerButtons;
        [SerializeField] private LabelQuestionController questionLabelController;
        [SerializeField] private AudioClip audioCorrect;
        [SerializeField] private MTCConfigSO mtcConfigSO;
        [SerializeField] private Transform transbuttonAnsGroup;
        [SerializeField] private CanvasGroup uiGuiding;
        [SerializeField] private Image handPoint;
        [SerializeField] private Image handHold;
        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);

            if (typeData == typeof(MTCInitStateDependency))
            {
                MTCInitStateDependency InitStateDependency = new MTCInitStateDependency();
                InitStateDependency.LabelQuestionController = questionLabelController;
                InitStateDependency.ButtonSpeakController = buttonSpeakerController;
                InitStateDependency.ButtonAnsControllers = buttonAnswerButtons;

                data = ConvertToType<T>(InitStateDependency);
            }
            else if (typeData == typeof(MTCGuidingDependency))
            {
                MTCGuidingDependency guidingDependency = new MTCGuidingDependency();
                guidingDependency.MTCGuidingConfig = mtcConfigSO.mtcGuidingConfig;
                guidingDependency.ButtonAnsControllers = buttonAnswerButtons;
                guidingDependency.TransButtonAnsGroup = transbuttonAnsGroup;
                guidingDependency.UiGuiding = uiGuiding;
                guidingDependency.HandPoint = handPoint;
                guidingDependency.HandHold = handHold;

                data = ConvertToType<T>(guidingDependency);
            }
            else if (typeData == typeof(MTCIntroStateDependency))
            {
                MTCIntroStateDependency introStateDependency = new MTCIntroStateDependency();
                introStateDependency.LabelQuestionController = questionLabelController;
                introStateDependency.ButtonSpeakerController = buttonSpeakerController;
                introStateDependency.ButtonAnsControllers = buttonAnswerButtons;

                data = ConvertToType<T>(introStateDependency);
            }
            else if (typeData == typeof(MTCDelayFinishStateDependency))
            {
                MTCDelayFinishStateDependency delayFinishDependency = new MTCDelayFinishStateDependency();
                delayFinishDependency.AudioClip = audioCorrect;
                data = ConvertToType<T>(delayFinishDependency);
            }
            else if (typeData == typeof(NextTurnStateDependency))
            {
                NextTurnStateDependency nextTurnStateDependency = new NextTurnStateDependency();
                nextTurnStateDependency.LabelQuestionController = questionLabelController;
                nextTurnStateDependency.ButtonSpeakController = buttonSpeakerController;
                nextTurnStateDependency.ButtonAnsControllers = buttonAnswerButtons;

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
