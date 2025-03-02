using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monkey.Game.Game4Demo
{
    public class AnswerSpawnerController : MonoBehaviour
    {
        public List<AnswerButtonController> AnswerPrefabs { get; set; }
        public bool IsAnyAnswerBeingDragged { get; set; } = false;
        public Transform spawnPoint;
        public float spawnRate = 2f;
        public float speed = 5f;
        private bool isPaused = false;

        private Queue<AnswerButtonController> activeAnswers = new Queue<AnswerButtonController>();

        void Start()
        {
            AnswerPrefabs = new List<AnswerButtonController>();
        }

        public void PauseConveyor()
        {
            Debug.Log("🛑 Băng chuyền TẠM DỪNG");
            isPaused = true;
        }

        public void ResumeConveyor()
        {
            Debug.Log("▶️ Băng chuyền CHẠY TIẾP");
            isPaused = false;
        }

        public void SpawnNewAnswer(float offsetX = 0)
        {
            if (activeAnswers.Count >= 4) return;

            int index = Random.Range(0, AnswerPrefabs.Count);
            Vector3 spawnPosition = transform.position + new Vector3(offsetX, 0, 0);

            float spacing = 2f; 
            spawnPosition.x += activeAnswers.Count * spacing;

            AnswerButtonController answer = Instantiate(AnswerPrefabs.ToArray()[index], spawnPosition, Quaternion.identity);
            answer.Initialize(this);
            answer.OriginalParent = transform;
            answer.transform.SetParent(transform, false);

            activeAnswers.Enqueue(answer);
            StartCoroutine(MoveAnswer(answer));
        }

        public void RequeueAnswer(AnswerButtonController answer)
        {
            activeAnswers.Enqueue(answer);

            float newXPosition = transform.position.x + (activeAnswers.Count * 2.5f);
            answer.transform.position = new Vector3(newXPosition, answer.transform.position.y, answer.transform.position.z);
        }



        IEnumerator MoveAnswer(AnswerButtonController answer)
        {
            while (answer.transform.position.x > -10)
            {
                while (isPaused)
                {
                    yield return null;
                }

                answer.transform.position += Vector3.left * speed * Time.deltaTime;
                yield return null;
            }

            if (!answer.IsBeingDragged)
            {
                RequeueAnswer(answer);
                //activeAnswers.Dequeue();
                //Destroy(answer.gameObject);
                //SpawnNewAnswer(); 
            }
        }

    }
}
