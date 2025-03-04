using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public Transform[] clouds; 
    public float speed = 0.2f; 
    public float resetPositionX = 7f; 
    public float destroyPositionX = -7f;

    void Update()
    {
        foreach (Transform cloud in clouds)
        {
            cloud.position += Vector3.left * speed * Time.deltaTime;

            if (cloud.position.x < destroyPositionX)
            {
                ResetCloudPosition(cloud);
            }
        }
    }

    void ResetCloudPosition(Transform cloud)
    {
        cloud.position = new Vector3(resetPositionX, cloud.position.y, cloud.position.z);
    }
}
