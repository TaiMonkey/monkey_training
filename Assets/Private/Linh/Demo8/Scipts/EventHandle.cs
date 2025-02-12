using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandle : MonoBehaviour
{
    private SkeletonGraphic tauVutruAnimation;
    private const string KEY_EVENT_SPACESHIP_FLY_3 = "Spaceship Fly 3";
    private const string KEY_EVENT_SPACESHIP_ON = "Spaceship On";
    private const string KEY_EVENT_CHEER= "Cheer";

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxSpaceshipFly3;
    [SerializeField] private AudioClip sfxSpaceshipOn;
    [SerializeField] private AudioClip sfxCheer;

    private void Start()
    {
        tauVutruAnimation = GetComponentInChildren<SkeletonGraphic>();
        tauVutruAnimation.AnimationState.Event += HandleEvent;

    }
    private void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        Debug.LogError(e.Data.Name);
        if (e.Data.Name.Equals(KEY_EVENT_SPACESHIP_FLY_3))
        {
            audioSource.clip = sfxSpaceshipFly3;
            audioSource.Play();
        }else if (e.Data.Name.Equals(KEY_EVENT_SPACESHIP_ON))
        {
            audioSource.clip = sfxSpaceshipOn;
            audioSource.Play();
        }
        else if (e.Data.Name.Equals(KEY_EVENT_CHEER))
        {
            audioSource.clip = sfxCheer;
            audioSource.Play();
        }

    }
}
