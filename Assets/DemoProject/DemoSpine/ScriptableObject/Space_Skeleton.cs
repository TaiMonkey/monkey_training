using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "space_Skeleton", menuName = "UI/space_Skeleton")]
public class Space_Skeleton : ScriptableObject
{
    public string spaceship_fly_3;
    public string spaceship_on;
    public string cheer;

    public AudioClip space_fly_3;
    public AudioClip space_on;
    public AudioClip space_cheer;

}
