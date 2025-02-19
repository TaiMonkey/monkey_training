using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BELT01MCPState
{
    None = 0,
    InitData,
    Intro,
    Guiding,
    Dragging,
    DragResult,
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


}