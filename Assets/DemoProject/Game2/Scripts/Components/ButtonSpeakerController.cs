using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.Game2Demo
{
    public class ButtonSpeakerController : MonoBehaviour
    { 
        public AudioClip AudioClip { get; set; }
        private UnityEngine.UI.Button button;
        
        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, AudioClip);
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
        }

        
    }
}
