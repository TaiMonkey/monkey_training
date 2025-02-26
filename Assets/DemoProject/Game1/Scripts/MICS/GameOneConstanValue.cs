
using MonkeyBase.Observer;
namespace Monkey.Game.GameOneDemo
{

    public class StateName
    {
        public enum Name
        {
            None = 0,
            Init = 1,
            Intro = 2,
            GamePlay = 3,
            NextTurn = 4,
            Guiding = 5,
        }

        public enum Status
        {
            InitStat = 0,
            InitFinish = 1,
            IntroStat = 2,
            IntroFinish = 3,
            PlayStart = 4,
            PlayFinish = 5,
            NexTurnStart = 4,
            NextTurnFinish = 5,
            GuidingStart = 6,
            GuidingFinish = 7,
            OnClick = 8,
        }
    }

    public class StaticValue
    {
        public static int CurrentTurn;
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

        public AnswerChanel (Type typeEvent, object data)
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