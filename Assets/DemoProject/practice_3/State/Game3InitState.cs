using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game3Demo
{
    public class Game3InitState : FSMState
    {
        private Game3InitStateDependency dependency;

        public override void SetUp(object data)
        {
            dependency = (Game3InitStateDependency)data;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }
    }
    public class Game3InitStateDependency
    {
        public QuesImageController QuesImageController { get; set; }
        public AnsTextController AnsTextController { get; set; }
    }

    public class Game3InitStateData
    {
    }


}


