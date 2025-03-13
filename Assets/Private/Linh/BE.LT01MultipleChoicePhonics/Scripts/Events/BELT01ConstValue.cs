using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BELT01MCPState
{
    None = 0,
    InitData,
    Intro,
    Click,
    Guiding,
    Dragging,
    DragResult,
    Nextturn,
    Outtro
}
public enum BELT01MCPStatusOfStateState
{
    None = 0,
    InitStateStart,
    InitStateEnd,
    IntroStateStart,
    IntroStateEnd,
    GuidingStateStart,
    GuidingStateEnd,
    DraggingStateStart,
    DraggingStateEnd,
    DragResualStart,
    DragResualEnd,
    ClickStart,
    ClickEnd,
    NextTurnStart,
    NextTurnEnd,
    Outrotart,
    OutroEnd,
}
public class BELT01ValueStatic
{
    public static int DragWrongCount { get; set; } = 0;
    public static int DragCorrectCount { get; set; } = 0;
}