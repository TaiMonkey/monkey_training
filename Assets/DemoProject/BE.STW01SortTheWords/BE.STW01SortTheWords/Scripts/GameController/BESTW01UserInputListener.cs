using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01UserInputListener : MonoBehaviour, EventListener<BESTW01InputChanner>
    {
        public void OnMMEvent(BESTW01InputChanner eventType)
        {
            switch (eventType.UserInput)
            {
                case BESTW01UserInput.ClickCard:
                    GameObject dataEventClickCard = (GameObject)eventType.Data;
                    BESTW01ClickStateEventData clickCardEvent = new BESTW01ClickStateEventData();
                    clickCardEvent.ObjectEvent = dataEventClickCard;
                    clickCardEvent.UserInput = BESTW01UserInput.ClickCard;
                    BESTW01HandleData.TriggerFinishState(BESTW01State.ClickObject, clickCardEvent);
                    break;
                case BESTW01UserInput.ClickBox:
                    GameObject dataEventClickBox = (GameObject)eventType.Data;
                    BESTW01ClickStateEventData clickBoEvent = new BESTW01ClickStateEventData();
                    clickBoEvent.ObjectEvent = dataEventClickBox;
                    clickBoEvent.UserInput = BESTW01UserInput.ClickBox;
                    BESTW01HandleData.TriggerFinishState(BESTW01State.ClickObject, clickBoEvent);
                    break;
                case BESTW01UserInput.SkipGuiding:
                    GameObject dataEventSkip = (GameObject)eventType.Data;
                    BESTW01ClickStateEventData clickSkipEvent = new BESTW01ClickStateEventData();
                    clickSkipEvent.ObjectEvent = dataEventSkip;
                    clickSkipEvent.UserInput = BESTW01UserInput.SkipGuiding;
                    BESTW01HandleData.TriggerFinishState(BESTW01State.ClickObject, clickSkipEvent);
                    break;
                case BESTW01UserInput.UnClick:
                    GameObject dataEventUnClick = (GameObject)eventType.Data;

                    BESTW01HandleData.TriggerFinishState(BESTW01State.PlayGame, null);
                    break;
                case BESTW01UserInput.Dragging:
                    GameObject dataEventDragging = (GameObject)eventType.Data;
                    BESTW01DraggingStateEventData draggingStateDataEvent = new BESTW01DraggingStateEventData();
                    draggingStateDataEvent.ObjectEvent = dataEventDragging;
                    BESTW01HandleData.TriggerFinishState(BESTW01State.DraggingObject, draggingStateDataEvent);
                    break;
               /* case BESTW01UserInput.DragMatching:
                    (GameObject card, GameObject box) dataEventDrag = ((GameObject card, GameObject box))eventType.Data;

                    BESTW01DragResultStateEventData dragResultStateDataEvent = new BESTW01DragResultStateEventData();
                    dragResultStateDataEvent.CardObject = dataEventDrag.card;
                    dragResultStateDataEvent.BoxObject = dataEventDrag.box;
                    BESTW01HandleData.TriggerFinishState(BESTW01State.DragResult, dragResultStateDataEvent);
                    break*/;

            }
        }
        private void OnEnable()
        {
            this.ObserverStartListening<BESTW01InputChanner>();
        }

        private void OnDisable()
        {
            this.ObserverStopListening<BESTW01InputChanner>();
        }
    }
}