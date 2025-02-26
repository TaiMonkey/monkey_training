using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.GameOneDemo
{
    public class ButtonSpeakerController : MonoBehaviour
    {
        public AudioClip AudioClip { private get; set; }

        private UnityEngine.UI.Button button;

        private void Awake()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(OnClick);
        }

        public void SetScale(Vector3 scale, float time, float delay)
        {
            transform.DOScale(scale, time).SetEase(Ease.OutBack).SetDelay(delay);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        private void OnClick()
        {
            Debug.LogError($"plau audio {AudioClip.name}");
        }
    }
}
