using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Monkey.Game.CF
{
    public class ButtonJarController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Button btn;
        [SerializeField] private RectTransform rect;
        [SerializeField] private Image image;
        public RectTransform Rect { get; set; }
        public int IsCorrectID { get; set; }
        private Sprite sprite;
        private CFButtonJarData databutton;
        private void Start()
        {

            btn = GetComponent<Button>();
            rect = GetComponent<RectTransform>();
        
        }
        public void InitData(CFButtonJarData data)
        {
            databutton = data;
            image.sprite = data.imange;
        }
        

        public void OnPointerDown(PointerEventData eventData)
        {
            SoundChannel sound = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, databutton.audioClip);
            ObserverManager.TriggerEvent<SoundChannel>(sound);
        }
    }
}