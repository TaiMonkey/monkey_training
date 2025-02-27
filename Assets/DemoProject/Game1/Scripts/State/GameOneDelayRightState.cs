using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.Game.GameOneDemo
{
    public class GameOneDelayRightState : FSMState
    {
        private int maxTurn;
        private CancellationTokenSource cts;
        private const int TIME_DELAY_START = 2000;
        public override void SetUp(object data)
        {
            throw new System.NotImplementedException();
        }

        public override async void OnEnter(object Data)
        {
            maxTurn = (int)Data;
            cts = new();
            await UniTask.Delay(TIME_DELAY_START, cancellationToken: cts.Token);

           if (StaticValue.CurrentTurn < (maxTurn - 1))
            {
                StateChanel stateChanel = new StateChanel(StateName.Status.NexTurnStart);
                ObserverManager.TriggerEvent(stateChanel);
            }
            else
            {
                UnityEngine.Debug.LogError("xxxx");
                // end game
            }
        }
    }

}
