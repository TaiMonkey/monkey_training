using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSlot : MonoBehaviour
{
    private SkeletonGraphic spaceshipAnimation;
    [SerializeField] private Sprite dataSlot1;
    [SerializeField] private Sprite dataSlot2;
    [SerializeField] private Material sourceMaterial;
    [SerializeField, SpineSkin] private string templateAttchmentsSkin;
    [SerializeField, SpineSlot] private string itemSlot1;
    [SerializeField, SpineAttachment(slotField: "itemSlot1", skinField: "baseSkinName")] private string itemKey1;
    [SerializeField, SpineSlot] private string itemSlot2;
    [SerializeField, SpineAttachment(slotField: "itemSlot2", skinField: "baseSkinName")] private string itemKey2;

    private Skin customSkin;
    
    void Start()
    {
        spaceshipAnimation = GetComponentInChildren<SkeletonGraphic>();
        customSkin = new Skin("custom skin");
        ChangeImageBySloteSpine(dataSlot1, itemSlot1, itemKey1);
        ChangeImageBySloteSpine(dataSlot2, itemSlot2, itemKey2);

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
    private void ChangeImageBySloteSpine(Sprite data, string itemSlot, string itemkey)
    {
        sourceMaterial = spaceshipAnimation.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial;
        var skeleton = spaceshipAnimation.Skeleton;
        var templateSkin = skeleton.Data.FindSkin(templateAttchmentsSkin);
        int slotIndex = skeleton.FindSlotIndex(itemSlot);
        Attachment templateItem = templateSkin.GetAttachment(slotIndex, itemkey);
        Attachment newItem = templateItem.GetRemappedClone(data, sourceMaterial, premultiplyAlpha:false, pivotShiftsMeshUVCoords: false, useOriginalRegionSize:true);
        if (newItem != null) customSkin.SetAttachment(slotIndex, itemkey, newItem);
        skeleton.SetSkin(customSkin);
        skeleton.SetSlotsToSetupPose();
    }


}
