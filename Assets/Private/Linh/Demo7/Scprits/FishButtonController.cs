using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
     private SkeletonGraphic fishAnimation;
    [SerializeField] private FishConfig fishConfig;
    [SerializeField, SpineSkin] private List<string> listAllSkin;

    void Start()
    {
        fishAnimation = GetComponentInChildren<SkeletonGraphic>();
        fishAnimation.AnimationState.SetAnimation(0, fishConfig.idle, true);
        SetSkin(fishAnimation, listAllSkin[0]);
        

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SetSkin(fishAnimation, listAllSkin[1]);
        fishAnimation.AnimationState.SetAnimation(0, fishConfig.usertap, false).Complete += (trackEntry) =>
        {
            fishAnimation.AnimationState.SetAnimation(0, fishConfig.userUntapLoop, true);
        };
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetSkin(fishAnimation, listAllSkin[2]);
        fishAnimation.AnimationState.SetAnimation(0, fishConfig.userUntap, false).Complete += (trackEntry) =>
        {
            fishAnimation.AnimationState.SetAnimation(0, fishConfig.idle, true);
        };
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
