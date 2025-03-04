using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01System : FSMSystem
    {
        private FSMState bESTW01InitState;
        private FSMState bESTW01IntroState;
        private FSMState bESTW01PlayState;
        private FSMState bESTW01ClickObjectState;
        private FSMState bESTW01DraggingState;
        private FSMState bESTW01DragResultState;
        private FSMState bESTW01EndGameState;


        private void Awake()
        {
            bESTW01InitState = new BESTW01InitState();
            bESTW01IntroState = new BESTW01IntroState();
            bESTW01PlayState = new BESTW01PlayState();
            bESTW01ClickObjectState = new BESTW01ClickState();
            bESTW01DraggingState = new BESTW01DraggingState();
            bESTW01DragResultState = new BESTW01DragResultState();
            bESTW01EndGameState = new BESTW01EndState();
        }
        public override void GotoState(string eventName, object data)
        {
            BESTW01State state = (BESTW01State)Enum.Parse(typeof(BESTW01State), eventName);
            switch (state)
            {
                case BESTW01State.InitData:
                    GotoState(bESTW01InitState, data);
                    break;
                case BESTW01State.IntroGame:
                    GotoState(bESTW01IntroState, data);
                    break;
                case BESTW01State.PlayGame:
                    GotoState(bESTW01PlayState, data);
                    break;
                case BESTW01State.ClickObject:
                    GotoState(bESTW01ClickObjectState, data);
                    break;
                case BESTW01State.DraggingObject:
                    GotoState(bESTW01DraggingState, data);
                    break;
                /*case BESTW01State.DragResult:
                    GotoState(bESTW01DragResultState, data);
                    break;*/
                case BESTW01State.EndGame:
                    GotoState(bESTW01EndGameState, data);
                    break;
            }
        }

        public override void SetupStateData<T>(T data)
        {
            if (data is Dependency dependency)
            {
                BESTW01InitStateObjectDependency initData = dependency.GetStateData<BESTW01InitStateObjectDependency>();
                bESTW01InitState.SetUp(initData);

                BESTW01IntroStateObjectDependency introData = dependency.GetStateData<BESTW01IntroStateObjectDependency>();
                bESTW01IntroState.SetUp(introData);

                BESTW01PlayStateObjectDependency playData = dependency.GetStateData<BESTW01PlayStateObjectDependency>();
                bESTW01PlayState.SetUp(playData);

                BESTW01ClickStateObjectDependency clickData = dependency.GetStateData<BESTW01ClickStateObjectDependency>();
                bESTW01ClickObjectState.SetUp(clickData);

                BESTW01DraggingStateObjectDependency draggingData = dependency.GetStateData<BESTW01DraggingStateObjectDependency>();
                bESTW01DraggingState.SetUp(draggingData);

                /*BESTW01DragResultStateObjectDependency dragResultData = dependency.GetStateData<BESTW01DragResultStateObjectDependency>();
                bESTW01DragResultState.SetUp(dragResultData);*/

                BESTW01EndStateObjectDependency endGameData = dependency.GetStateData<BESTW01EndStateObjectDependency>();
                bESTW01EndGameState.SetUp(endGameData);
            }
        }

    }
}