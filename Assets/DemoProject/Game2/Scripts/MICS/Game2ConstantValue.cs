using System.Collections;
using System.Collections.Generic;
using MonkeyBase.Observer;
using UnityEngine;

namespace Monkey.Game.Game2Demo
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
    }

    public struct StateChanel: EventListener<StateChanel>
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

    public struct AnswerChanel : EventListener<AnswerChanel>
    {
        public enum Type
        {
            None = 0,
            Answer = 1
        }

        public object Data { get; private set; }
        public Type TypeEvent { get; private set; }

        public AnswerChanel(Type typeEvent, object data)
        {
            this.Data = data;
            this.TypeEvent = typeEvent;
        }
        public void OnMMEvent(AnswerChanel eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}
