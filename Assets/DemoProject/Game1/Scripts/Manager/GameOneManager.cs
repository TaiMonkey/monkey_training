
using MonkeyBase.Observer;
namespace Monkey.Game.GameOneDemo
{
    public class GameOneManager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            //Start Fake setup data in adapter
            SetData("");
            // End Fake setup data

            this.ObserverStartListening<StateChanel>();

            base.Start();

            fSMSystem.SetupStateData(dependency);



            GameOneInitStateData initStateData = adapter.GetData<GameOneInitStateData>(StaticValue.CurrentTurn);
            fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<StateChanel>();
        }

        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }

        public void OnMMEvent(StateChanel eventType)
        {
            switch(eventType.StatusOfState)
            {
                case StateName.Status.InitFinish:
                    GameOneIntroStateData introStateData = adapter.GetData<GameOneIntroStateData>(0);
                    fSMSystem.GotoState(StateName.Name.Intro.ToString(), introStateData);
                    break;
                case StateName.Status.GuidingStart:
                    fSMSystem.GotoState(StateName.Name.Guiding.ToString(), null);
                    break;
                case StateName.Status.IntroFinish:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), null);
                    break;
                case StateName.Status.NexTurnStart:
                    fSMSystem.GotoState(StateName.Name.NextTurn.ToString(), null);
                    break;
                case StateName.Status.NextTurnFinish:
                    GameOneInitStateData initStateData = adapter.GetData<GameOneInitStateData>(StaticValue.CurrentTurn);
                    fSMSystem.GotoState(StateName.Name.Init.ToString(), initStateData);
                    break;
                case StateName.Status.PlayFinish:
                    int maxTurn = adapter.GetMaxTurn();
                    fSMSystem.GotoState(StateName.Name.DelayRight.ToString(), maxTurn);
                    break;
                case StateName.Status.GuidingFinish:
                    fSMSystem.GotoState(StateName.Name.GamePlay.ToString(), eventType.Data);
                    break;
            }
        }
    }
}
