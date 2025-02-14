using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMangaerController : MonoBehaviour
{
    [SerializeField] private GameObject fishButton;
    private VerticalLayoutGroup verticalLayoutGroup;
    private List<FishButtonController> listButton;
    private Coroutine coroutine;
    [SerializeField] private FishConfig fishConfig;
    void Start()
    {
        listButton = new List<FishButtonController>();
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(fishButton, transform, false);
            obj.transform.localScale = Vector3.one;
            FishButtonController fishButtonController = obj.GetComponent<FishButtonController>();
            listButton.Add(fishButtonController);
            int skinIndex = i % fishConfig.listSkins.Count; // Luân phiên skin
            SetSkin(fishButtonController.FishAnimation, fishConfig.listSkins[skinIndex]);
        }

        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        coroutine = StartCoroutine(DisableLayout());


    }

    private IEnumerator DisableLayout()
    {
        yield return new WaitForEndOfFrame();
        verticalLayoutGroup.enabled = false;
    }
    public void SetSkin(SkeletonGraphic skeletonGraphic, string skinName)
    {
        var skeleton = skeletonGraphic.Skeleton;
        var skin = skeleton.Data.FindSkin(skinName);
        if (skin == null) return;
        skeleton.SetSkin(skin);
        skeleton.SetToSetupPose();
        skeletonGraphic.AnimationState.Apply(skeleton);
    }
}
