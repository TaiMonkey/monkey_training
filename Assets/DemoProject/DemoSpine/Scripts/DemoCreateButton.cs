using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCreateButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Fish_Skeleton fish_Skeleton;
    private Transform demoCreateButton;
    private List<string> availableSkins;
    void Start()
    {
        demoCreateButton = GetComponent<Transform>();
        availableSkins = new List<string>(fish_Skeleton.skins);
        createButton();
    }

    void createButton()
    {
        for(int i = 0; i <=3; i++)
        {
            if (availableSkins.Count == 0) return;
            int randomIndex = Random.Range(0, availableSkins.Count);
            string selectedSkin = availableSkins[randomIndex];
           // availableSkins.RemoveAt(randomIndex);

            GameObject newBtn = Instantiate(buttonPrefab, demoCreateButton, false);
            SkeletonGraphic skeleton = newBtn.GetComponentInChildren<SkeletonGraphic>();
            newBtn.name = "Btn_Fish" + i;

            DemoAnimState animState = newBtn.GetComponent<DemoAnimState>();
            animState.SetSkin(skeleton, selectedSkin);
        }
    }

}
