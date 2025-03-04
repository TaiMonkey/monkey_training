using MonkeyBase.Observer;


namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Manager : GameManager, EventListener<BESTW01DataChanner>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            this.ObserverStartListening<BESTW01DataChanner>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            this.ObserverStopListening<BESTW01DataChanner>();
        }

        public override void SetData<T>(T data)
        {
            base.SetData(data);
            adapter.SetData(data);
        }


        protected override void Start()
        {
            base.Start();
            fSMSystem.SetupStateData(dependency);
            BESTW01DataChanner dataChanner = new BESTW01DataChanner(BESTW01State.InitData, null);
            ObserverManager.TriggerEvent(dataChanner);
        }

        public void OnMMEvent(BESTW01DataChanner eventType)
        {
            (string eventName, object data) navigatorData = navigator.GetData(adapter, eventType.EventName.ToString(), eventType.Data); ;
            fSMSystem.GotoState(navigatorData.eventName, navigatorData.data);
        }
    }
}