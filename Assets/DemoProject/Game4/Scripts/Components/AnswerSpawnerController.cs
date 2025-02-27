using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] answerPrefabs;
    public Transform spawnPoint;
    public float spawnRate = 2f;
    public float speed = 5f;

    private Queue<GameObject> activeAnswers = new Queue<GameObject>();
    private int maxAnswers = 4;
    private float spawnSpacing = 3f;
    void Start()
    {
        for (int i = 0; i < maxAnswers; i++)
        {
            SpawnNewAnswer(i * spawnSpacing * 92);
        }
    }
    void SpawnNewAnswer(float offsetX = 0)
    {
        // Chọn ngẫu nhiên 1 loại đáp án
        int index = Random.Range(0, answerPrefabs.Length);
        Vector3 spawnPosition = spawnPoint.position + new Vector3(offsetX, 0, 0); // Dịch sang phải một khoảng
        GameObject answer = Instantiate(answerPrefabs[index], spawnPosition, Quaternion.identity);
        answer.transform.SetParent(spawnPoint, false);

        activeAnswers.Enqueue(answer); // Thêm vào hàng đợi
        StartCoroutine(MoveAnswer(answer));
    }

    IEnumerator MoveAnswer(GameObject answer)
    {
        while (answer.transform.position.x > -10) // Khi chưa ra khỏi màn hình
        {
            answer.transform.position += Vector3.left * speed * Time.deltaTime;
            yield return null;
        }

        // Khi thẻ ra khỏi màn hình, xóa nó khỏi danh sách
        activeAnswers.Dequeue();
        Destroy(answer);

        // Spawn thẻ mới để đảm bảo luôn có 4 thẻ trên màn hình
        SpawnNewAnswer();
    }
}
