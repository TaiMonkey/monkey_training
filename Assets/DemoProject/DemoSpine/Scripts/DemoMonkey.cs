using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using Spine.Unity.AttachmentTools;

public class DemoMonkey : MonoBehaviour
{
    private SkeletonGraphic monkeySkeleton;
    [SerializeField] monkey_Skeleton monkey_Skeleton;
    [SerializeField, SpineSkin] private string templateAttackmentSkin;
    public Skin customSkin;

    void Start()
    {
        monkeySkeleton = GetComponentInChildren<SkeletonGraphic>();
        customSkin = new Skin("custome skin");
        ChangeImageBySlotSpine(monkey_Skeleton.dataSlot, monkey_Skeleton.itemSlot[0], monkey_Skeleton.itemKey[0]);
        ChangeImageBySlotSpine(monkey_Skeleton.dataSlot, monkey_Skeleton.itemSlot[1], monkey_Skeleton.itemKey[1]);
    }

    private void ChangeImageBySlotSpine(Sprite data, string itemSlot, string itemKey)
    {
        monkey_Skeleton.sourceMaterial = monkeySkeleton.skeletonDataAsset.atlasAssets[0].PrimaryMaterial;
        var skeleton = monkeySkeleton.Skeleton;
        var templateSkin = skeleton.Data.FindSkin(templateAttackmentSkin);
        int slotIndex = skeleton.FindSlotIndex(itemSlot);
        Attachment templateItem = templateSkin.GetAttachment(slotIndex, itemKey);
        Attachment newItem = templateItem.GetRemappedClone(data, monkey_Skeleton.sourceMaterial, premultiplyAlpha:false, pivotShiftsMeshUVCoords: false, useOriginalRegionSize:true);
        if (newItem != null) customSkin.SetAttachment(slotIndex, itemKey, newItem);
        skeleton.SetSkin(customSkin);
        skeleton.SetSlotsToSetupPose();
    }

}
