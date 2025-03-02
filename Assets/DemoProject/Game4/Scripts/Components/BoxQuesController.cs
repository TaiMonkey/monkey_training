using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MonkeyBase.Observer;
using Spine.Unity;

namespace Monkey.Game.Game4Demo
{
    public class BoxQuesController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI TextQuestion;
        [SerializeField] AudioClip AudioClip;
        [SerializeField] int index;
        private SkeletonGraphic skeleton;
        private UnityEngine.UI.Button button;
        private const string TAP = "Tap hop 1 - Lv0";
        private const string TAP_2 = "Tap hop 2 - Lv0";

        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            skeleton = GetComponentInChildren<SkeletonGraphic>();
            button.onClick.AddListener(OnClick);
        }

        public void SetTextQuestion(string text)
        {
            this.TextQuestion.text = text;
        }

        public void SetAudio(AudioClip audioClip)
        {
            this.AudioClip = audioClip;
        }

        public void OnClick()
        {
            if (index == 1)
            {
                skeleton.AnimationState.SetAnimation(0, TAP, false);
            }
            else
            {
                skeleton.AnimationState.SetAnimation(0, TAP_2, false);
            }
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
        }
    }
}
