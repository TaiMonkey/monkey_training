using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.GameTest
{
    public class GameTestAdapder : Adapter
    {
        public override T GetData<T>(int turn)
        {
            throw new System.NotImplementedException();
        }

        public override int GetMaxTurn()
        {
            return 0;
        }

        public override void SetData<T>(T data)
        {
            throw new System.NotImplementedException();
        }

    }
}
