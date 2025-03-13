using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DependencyBuoi3 : Dependency
{
    [SerializeField] private DemoConfigSO demoConfigSO;
    [SerializeField] private List<Button> listButton;
    [SerializeField] private Image imageOutLine;
    [SerializeField] private DemoButtonConfig buttonConfig;
    [SerializeField] private List<DemoFishButton> listFish;
    [SerializeField] private Transform buttonGroup;
    [SerializeField] private CanvasGroup uiGuiding;
    [SerializeField] private Image handLong;
    [SerializeField] private Image handShort;

    public override T GetStateData<T>()
    {
        T data;
        Type typeData = typeof(T);
        if (typeData == typeof(InitStateBuoi3ObjectDependency))
        {
            InitStateBuoi3ObjectDependency initStateDependency = new InitStateBuoi3ObjectDependency();
            initStateDependency.Buttons = listButton;
            initStateDependency.ImageOutline = imageOutLine;
            initStateDependency.ListFish = listFish;
            data = ConvertToType<T>(initStateDependency);
        }
        else if (typeData == typeof(IntroStateBuoi3ObjectDependency))
        {
            IntroStateBuoi3ObjectDependency introStateDependency = new IntroStateBuoi3ObjectDependency();
            introStateDependency.IntroConfig = demoConfigSO.demoIntroConfig;
            introStateDependency.ListFish = listFish;
            data = ConvertToType<T>(introStateDependency);
        }
        else if (typeData == typeof(DraggingStateBuoi3ObjectDependency))
        {
            DraggingStateBuoi3ObjectDependency draggingDependency = new DraggingStateBuoi3ObjectDependency();
            draggingDependency.ListFish = listFish;
            data = ConvertToType<T>(draggingDependency);
        }
        else if(typeData == typeof(DragResultBuoi3ObjectDependency))
        {
            DragResultBuoi3ObjectDependency dragResultDependency = new DragResultBuoi3ObjectDependency();
            dragResultDependency.TransImage = imageOutLine.transform;

            data = ConvertToType<T>(dragResultDependency);
        }else if(typeData == typeof(GuidingStateBuoi3ObjectDependency))
        {
            GuidingStateBuoi3ObjectDependency guidingStateBuoi3Dependency = new GuidingStateBuoi3ObjectDependency();
            guidingStateBuoi3Dependency.DemoGuidingConfig = demoConfigSO.demoGuidingConfig;
            guidingStateBuoi3Dependency.ListFish = listFish;
            guidingStateBuoi3Dependency.TransImage = imageOutLine.transform;
            guidingStateBuoi3Dependency.ButtonGroup = buttonGroup;
            guidingStateBuoi3Dependency.UiGuiding = uiGuiding;
            guidingStateBuoi3Dependency.HandLong = handLong;
            guidingStateBuoi3Dependency.HandShort = handShort;

            data = ConvertToType<T>(guidingStateBuoi3Dependency);
        }
        else
        {
            data = ConvertToType<T>(null);
        }

        return data;
    }
}
