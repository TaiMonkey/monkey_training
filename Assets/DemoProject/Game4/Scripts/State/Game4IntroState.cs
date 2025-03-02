using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class Game4IntroState : FSMState
    {
        private Game4IntroStateDependency dependency;

        private int maxAnswers = 4;
        private float spawnSpacing = 3f;

        public override void SetUp(object data)
        {
            dependency = (Game4IntroStateDependency)data;
        }

        public override void OnEnter(object data)
        {
            for (int i = 0; i < dependency.AnswerButtonControllers.Count; i++)
            {
                dependency.AnswerSpawnerController.AnswerPrefabs.Add(dependency.AnswerButtonControllers[i]);
            }

            dependency.AnswerSpawnerController.InitializeAnswers();

            //for(int i = 0; i < maxAnswers; i++)
            //{
            //    dependency.AnswerSpawnerController.SpawnNewAnswer(i * spawnSpacing * 92);
            //}
        }
    }

    public class Game4IntroStateDependency
    {
        public List<AnswerButtonController> AnswerButtonControllers { get; set; }
        public AnswerSpawnerController AnswerSpawnerController { get; set; }
    }
}
