using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemoTauVuTru : MonoBehaviour
{
    private SkeletonGraphic spaceshipSkeleton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Space_Skeleton space_Skeleton;

    // Start is called before the first frame update
    void Start()
    {
        spaceshipSkeleton = GetComponentInChildren<SkeletonGraphic>();
        spaceshipSkeleton.AnimationState.Event += handeEvent;
    }

    private void handeEvent(TrackEntry trackEntry, Spine.Event e)
    {
        Debug.LogError(e.Data.Name);
        if(e.Data.Name.Equals(space_Skeleton.spaceship_fly_3)) {
            audioSource.clip = space_Skeleton.space_fly_3;
            audioSource.Play();
        }
        if (e.Data.Name.Equals(space_Skeleton.spaceship_on))
        {
            audioSource.clip = space_Skeleton.space_on;
            audioSource.Play();
        }
        if (e.Data.Name.Equals(space_Skeleton.cheer))
        {
            audioSource.clip = space_Skeleton.space_cheer;
            audioSource.Play();
        }
    }

}
