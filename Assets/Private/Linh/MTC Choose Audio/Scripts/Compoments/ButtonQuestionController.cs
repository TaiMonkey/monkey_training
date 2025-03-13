using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTCCA
{
    public class ButtonQuestionController : MonoBehaviour
    {
        private Button button;
        public AudioClip AudioClip { get; set; }
        [SerializeField] private Image image;
       
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, AudioClip, () => { });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Click, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }
        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
        public void SetScale(Vector3 scale, float time)
        {
            transform.DOScale(scale, time).SetEase(Ease.Linear);
        }
        public void SetImage(Sprite Image)
        {
            image.sprite = Image;
        }

    }
}
