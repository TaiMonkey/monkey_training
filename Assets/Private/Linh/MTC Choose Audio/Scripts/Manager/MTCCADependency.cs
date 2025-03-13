using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTCCA
{
    public class MTCCADependency : Dependency
    {
        [SerializeField] private List<ButtonAnsController> buttonAnsButtons;
        [SerializeField] private BGContentController bGContentController;
        [SerializeField] private ButtonQuestionController buttonQuestionController;
        [SerializeField] private KhungAnsController khungAnsController;
        [SerializeField] private MTCCAConfigSO mtccaConfigSO;
        [SerializeField] private SkeletonGraphic planeAnimation;
        [SerializeField] private Transform transbuttonAnsGroup;
        [SerializeField] private CanvasGroup uiGuiding;
        [SerializeField] private Image handPoint;
        [SerializeField] private Image handHold;


        public override T GetStateData<T>()
        {
            T data;
            Type typeData = typeof(T);
            if (typeData == typeof(MTCCAInitStateDependency))
            {
                MTCCAInitStateDependency initDependency = new MTCCAInitStateDependency();
                initDependency.ButtonQuestionController = buttonQuestionController;
                initDependency.ButtonAnsControllers = buttonAnsButtons;
                initDependency.BGContentController = bGContentController;
                initDependency.KhungAnsController = khungAnsController;
                data = ConvertToType<T>(initDependency);
            }
            else if (typeData == typeof(MTCCAIntroStateDependency))
            {
                MTCCAIntroStateDependency introDependency = new MTCCAIntroStateDependency();
                introDependency.ButtonQuestionController = buttonQuestionController;
                introDependency.ButtonAnsControllers = buttonAnsButtons;
                introDependency.BGContentController = bGContentController;
                introDependency.KhungAnsController = khungAnsController;
                introDependency.MTCCAIntroConfig = mtccaConfigSO.introConfig;



                data = ConvertToType<T>(introDependency);
            }
            else if (typeData == typeof(MTCCAPlayStateDependency))
            {
                MTCCAPlayStateDependency playDependency = new MTCCAPlayStateDependency();
                playDependency.TransImage = khungAnsController.transform;
                playDependency.KhungAnsController = khungAnsController;
                data = ConvertToType<T>(playDependency);

            }
            else if (typeData == typeof(MTCCAGuidingStateDependency))
            {
                MTCCAGuidingStateDependency guidingDependency = new MTCCAGuidingStateDependency();
                guidingDependency.MTCCAGuidingConfig = mtccaConfigSO.guidingConfig;
                guidingDependency.ButtonAnsControllers = buttonAnsButtons;
                guidingDependency.TransButtonAnsGroup = transbuttonAnsGroup;
                guidingDependency.TransKhungAns = khungAnsController.transform;
                guidingDependency.UiGuiding = uiGuiding;
                guidingDependency.HandHold = handHold;
                guidingDependency.HandPoint = handPoint;
                data = ConvertToType<T>(guidingDependency);

            }
            else if (typeData == typeof(MTCCADelayFinishStateDependency))
            {
                MTCCADelayFinishStateDependency delayDependency = new MTCCADelayFinishStateDependency();
                delayDependency.PlaneAnimation = planeAnimation;
                delayDependency.Anim = mtccaConfigSO.anim;
                data = ConvertToType<T>(delayDependency);

            }
            else if (typeData == typeof(MTCCANextTurnStateDependency))
            {
                MTCCANextTurnStateDependency nextTurnDependency = new MTCCANextTurnStateDependency();
                nextTurnDependency.PlaneAnimation = planeAnimation;
                nextTurnDependency.ButtonQuestionController = buttonQuestionController;
                nextTurnDependency.ButtonAnsControllers = buttonAnsButtons;
                data = ConvertToType<T>(nextTurnDependency);

            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }


       
    }
}
