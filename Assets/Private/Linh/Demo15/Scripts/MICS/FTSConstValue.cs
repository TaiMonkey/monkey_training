using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.FTS
{
    public class StateName
    {
        public enum Name
        {
            None = 0,
            Init = 1,
            Intro = 2,
            Guidding = 3,
            GamePlay = 4,
            NextTurn = 5,
        }

        public enum Status
        {
            InitStat = 0,
            InitFinish = 1,
            IntroStat = 2,
            IntroFinish = 3,
            GuiddingStart = 4,
            GuiddingFinish = 5,
            PlayStart = 6,
            PlayFinish = 7,
            NexTurnStart = 8,
            NextTurnFinish = 9,
        }
    }

    public class StaticValue
    {
        public static int CurrentTurn=0;
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