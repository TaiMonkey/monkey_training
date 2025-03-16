using MonkeyBase.Observer;

namespace Monkey.Game.StoryElement
{
    public class StoryElementManager : GameManager, EventListener<StateChanel>
    {
        protected override void Start()
        {
            SetData("");
            base.Start();

            this.ObserverStartListening<StateChanel>();

            fSMSystem.SetupStateData(dependency);

            fSMSystem.GotoState(StateName.Name.Init.ToString(), null);
        }

        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ObserverStopListening<StateChanel>();
        }

        public void OnMMEvent(StateChanel eventType)
        {
            switch (eventType.StatusOfState)
            {
                case StateName.Status.InitFinish:
                    break;
            }
        }
    }
}
