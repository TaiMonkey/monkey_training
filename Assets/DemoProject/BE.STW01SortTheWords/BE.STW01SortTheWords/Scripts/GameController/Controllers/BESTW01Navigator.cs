using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Navigator : Navigator
    {
        private float aspectRatio = 0;
        private float timeStart = 0f;
        private float timeEnd = 0f;
        private int dragItemCount = 0;

        private void Awake()
        {
            timeStart = Time.realtimeSinceStartup;
        }


        public override (string, object) GetData(Adapter adapter, string eventName, object eventData)
        {
            BESTW01State stateReturn = BESTW01State.InitData;
            object ObjectDataReturn = null;
            BESTW01State currentState = (BESTW01State)Enum.Parse(typeof(BESTW01State), eventName);
            LogMe.Log("Lucanhtai currentState: " + currentState.ToString());
            if (aspectRatio == 0) aspectRatio = (float)Screen.width / Screen.height;

            switch (currentState)
            {
                case BESTW01State.InitData:
                    stateReturn = BESTW01State.InitData;
                    BESTW01HandleData.CurrentTurnBoxLeft = 0;
                    BESTW01HandleData.CurrentTurnBoxRight = 0;
                    BESTW01InitStateData initStateData = adapter.GetData<BESTW01InitStateData>(turn);
                    initStateData.AspectRatio = aspectRatio;
                    ObjectDataReturn = initStateData;
                    break;
                case BESTW01State.IntroGame:
                    stateReturn = BESTW01State.IntroGame;
                    break;
                case BESTW01State.PlayGame:
                    stateReturn = BESTW01State.PlayGame;
                    break;
                case BESTW01State.ClickObject:
                    stateReturn = BESTW01State.ClickObject;
                    BESTW01ClickStateData clickStateData = new BESTW01ClickStateData();
                    clickStateData.EventData = (BESTW01ClickStateEventData)eventData;
                    ObjectDataReturn = clickStateData;
                    break;
                case BESTW01State.DraggingObject:
                    dragItemCount++;
                    stateReturn = BESTW01State.DraggingObject;
                    BESTW01DraggingStateEventData draggingStateDataEvent = new BESTW01DraggingStateEventData();
                    draggingStateDataEvent = (BESTW01DraggingStateEventData)eventData;
                    ObjectDataReturn = draggingStateDataEvent;
                    break;
                /*case BESTW01State.DragResult:
                    stateReturn = BESTW01State.DragResult;
                    BESTW01DragResultStateData dragResultStateData = new BESTW01DragResultStateData();
                    dragResultStateData.EventData = (BESTW01DragResultStateEventData)eventData;
                    ObjectDataReturn = dragResultStateData;
                    break;*/

               /* case BESTW01State.DragCorrect:
                    stateReturn = BESTW01State.PlayGame;
                    BESTW01TypeBox typeBoxEvent = (BESTW01TypeBox)eventData;

                    if (typeBoxEvent == BESTW01TypeBox.Left) turnBoxLeft++;
                    if (typeBoxEvent == BESTW01TypeBox.Right) turnBoxRight++;

                    break;*/

                case BESTW01State.EndGame:
                    stateReturn = BESTW01State.EndGame;

                    break;
                case BESTW01State.FinishGame:
                    stateReturn = BESTW01State.FinishGame;
                    BESTW01HandleData.CurrentTurnBoxLeft = 0;
                    BESTW01HandleData.CurrentTurnBoxRight = 0;
                    SendEventChooseAnswer(adapter.GetData<BESTW01GamePlayData>(0));
                    SoundChannel soundData = new SoundChannel(SoundChannel.STOP_ALL_SOUND_BY_DESTROY, null, null, 0, false);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);

                    timeEnd = Time.realtimeSinceStartup;
                    List<UserEndGameData.Word> wordList = adapter.GetData<List<UserEndGameData.Word>>(turn);

                    UserEndGameData userEndGameData = new UserEndGameData();
                    userEndGameData.TimeSpent = Mathf.CeilToInt(timeEnd - timeStart);
                    userEndGameData.MaxTurn = adapter.GetMaxTurn();
                    userEndGameData.ScoresList = new List<UserEndGameData.Scores>();
                    userEndGameData.PhonicList = null;
                    userEndGameData.VideoList = null;
                    userEndGameData.WordList = wordList;
                    //Score
                    UserEndGameData.Scores scores = new UserEndGameData.Scores();
                    scores.Score = CalculateDragScore(dragItemCount);
                    userEndGameData.ScoresList.Add(scores);

                    EventUserPlayGameChanel userEvent = new EventUserPlayGameChanel(EventUserPlayGameChanel.UserEvent.FinishGame, userEndGameData);
                    ObserverManager.TriggerEvent(userEvent);
                    break;
                default:
                    ObjectDataReturn = null;
                    break;
            }
            return (stateReturn.ToString(), ObjectDataReturn);

        }
        public int CalculateDragScore(int numberOfDrag)
        {
            if (numberOfDrag > 20)
            {
                return 20;
            }
            else if (numberOfDrag >= 17 && numberOfDrag <= 20)
            {
                return 40;
            }
            else if (numberOfDrag >= 13 && numberOfDrag <= 16)
            {
                return 60;
            }
            else if (numberOfDrag >= 11 && numberOfDrag <= 12)
            {
                return 80;
            }
            else
            {
                return 100;
            }
        }
        private void SendEventChooseAnswer(BESTW01GamePlayData gamePlayData)
        {
            UserChooseAnswerData chooseAnswerData = new UserChooseAnswerData();
            chooseAnswerData.ListAnswerData = new List<UserChooseAnswerData.AnswerData>();
            string answerType = UserChooseAnswerData.AnswerType.Passive.ToString().ToLower();

            UserChooseAnswerData.AnswerData answerFirstBoxData = new UserChooseAnswerData.AnswerData();
            answerFirstBoxData.Target = gamePlayData.cardDataLeft.text;
            answerFirstBoxData.WordID = gamePlayData.cardDataLeft.wordId;
            answerFirstBoxData.WordType = gamePlayData.cardDataLeft.wordDataCard.Type.ToString();
            answerFirstBoxData.TypeValue = answerType;
            chooseAnswerData.ListAnswerData.Add(answerFirstBoxData);

            UserChooseAnswerData.AnswerData answerSecondBoxData = new UserChooseAnswerData.AnswerData();
            answerSecondBoxData.Target = gamePlayData.cardDataRight.text;
            answerSecondBoxData.WordID = gamePlayData.cardDataRight.wordId;
            answerSecondBoxData.WordType = gamePlayData.cardDataRight.wordDataCard.Type.ToString();
            answerSecondBoxData.TypeValue = answerType;
            chooseAnswerData.ListAnswerData.Add(answerSecondBoxData);

            EventUserPlayGameChanel userEventCorrect = new EventUserPlayGameChanel(EventUserPlayGameChanel.UserEvent.OtherReport, chooseAnswerData);
            ObserverManager.TriggerEvent(userEventCorrect);
        }
        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}