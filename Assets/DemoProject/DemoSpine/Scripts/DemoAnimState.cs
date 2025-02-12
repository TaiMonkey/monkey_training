using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemoAnimState : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private SkeletonGraphic fishAnimState;
    [SerializeField] private Fish_Skeleton fish_Skeleton;

    // Start is called before the first frame update
    void Start()
    {
        fishAnimState = GetComponentInChildren<SkeletonGraphic>();
        fishAnimState.AnimationState.SetAnimation(0, fish_Skeleton.idie, true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fishAnimState.AnimationState.SetAnimation(0, fish_Skeleton.user_tap, false).Complete += (trackEntry) => {
            fishAnimState.AnimationState.SetAnimation(0, fish_Skeleton.user_tap_loop, true);
        };
        SetSkin(fishAnimState, fish_Skeleton.than_xanh);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fishAnimState.AnimationState.SetAnimation(0, fish_Skeleton.user_untap, false).Complete += (trackEntry) => {
            fishAnimState.AnimationState.SetAnimation(0, fish_Skeleton.idie, true);
        };
        SetSkin(fishAnimState, fish_Skeleton.than_hong);
    }

    public void SetSkin(SkeletonGraphic skeletonGraphic, string skinName)
    {
        var skeleton = skeletonGraphic.Skeleton;
        var skin = skeleton.Data.FindSkin(skinName);
        if (skin == null) return;
        skeleton.SetSkin(skin);
        skeleton.SetBonesToSetupPose();
        skeletonGraphic.AnimationState.Apply(skeleton);
    }

}
