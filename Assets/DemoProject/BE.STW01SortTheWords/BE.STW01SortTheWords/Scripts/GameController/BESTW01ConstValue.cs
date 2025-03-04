using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public enum BESTW01State
    {
        InitData,
        IntroGame,
        PlayGame,
        DragResult,
        DragCorrect,
        DragWrong,
        ClickObject,
        DraggingObject,
        EndGame,
        FinishGame
    }

    public enum BESTW01UserInput
    {
        ClickCard,
        ClickBox,
        UnClick,
        SkipGuiding,
        DragCardCarousel,
        UnDragCardCarousel,
        Dragging,
        DragMatching,
    }
    public enum BESTW01TypeBox
    {
       Left = 1,
       Right
    }
}