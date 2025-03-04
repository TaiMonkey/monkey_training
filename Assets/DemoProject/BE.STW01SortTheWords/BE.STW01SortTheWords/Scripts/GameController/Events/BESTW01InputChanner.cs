using MonkeyBase.Observer;


namespace Monkey.MJ5.BESTW01SortTheWords
{
    public struct BESTW01InputChanner : EventListener<BESTW01InputChanner>
    {
        public BESTW01UserInput UserInput;
        public object Data;

        public BESTW01InputChanner(BESTW01UserInput userInput, object data)
        {
            this.UserInput = userInput;
            this.Data = data;
        }

        public void OnMMEvent(BESTW01InputChanner eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}