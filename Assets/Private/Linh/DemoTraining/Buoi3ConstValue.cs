using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Buoi3State
{
    None = 0,
    InitData,
    Intro,
    Guiding,
    Click,
    Draggingg, 
    DragResult,
    NextTurn,
    Outro
}

public enum Buoi3StatusOfStateState
{
    None = 0,
    InitStateStart,
    InitStateEnd,
    IntroStart,
    IntroEnd,
    ClickStart,
    ClickEnd,
    GuidingStart,
    GuidingEnd,
    DragginggStart,
    DragginggEnd,
    DragResultStart,
    DragResultEnd,
    NextTurnStart,
    NextTurnEnd,
    Outrotart,
    OutroEnd,
}

public class Buoi3ValueStatic
{
    public static int DragWrongCount { get; set; } = 0;
    public static int DragCorrectCount { get; set; } = 0;


}