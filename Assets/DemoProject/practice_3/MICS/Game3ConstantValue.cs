using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game3Demo
{
    public class StateName
    {
        public enum Name
        {
            None = 0,
            Init = 1,
            Intro = 2,
            GamePlay = 3,
            Guiding = 4
        }

        public enum Status
        {
            InitStart = 0,
            InitEnd = 1,
            IntroStart = 2,
            IntroEnd = 3,
            PlayStart = 4,
            PlayEnd = 5,
            GuidingStart = 6,
            GuidingEnd = 7,
            OnClick = 8
        }
    }

    public class StaticValue
    {
        public static int CountWrong = 0;
        public static int CurrentTurn = 0;
    }

    public struct StateChanel : EventListener<StateChanel>
    {
        public object Data;
        public StateName.Status StatusOfState;

        public StateChanel(StateName.Status statusOfState)
        {
            Data = null;
            StatusOfState = statusOfState;
        }

        public StateChanel(StateName.Status statusOfState, object data)
        {
            Data = data;
            StatusOfState = statusOfState;
        }

        public void OnMMEvent(StateChanel eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}
