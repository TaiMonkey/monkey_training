using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
    }
}
