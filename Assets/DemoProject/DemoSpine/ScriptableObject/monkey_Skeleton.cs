using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "monkey_Skeleton", menuName = "UI/monkey_Skeleton")]
public class monkey_Skeleton : ScriptableObject
{
    public Sprite dataSlot;
    public string[] itemSlot;
    public string[] itemKey;
    public Material sourceMaterial;

}
