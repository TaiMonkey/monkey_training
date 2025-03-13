using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.FTS
{
    public class SharkController : MonoBehaviour
    {
        private Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        public void SetScale(Vector3 scale, float time, float delay)
        {
            transform.DOScale(scale, time).SetEase(Ease.Linear).SetDelay(delay);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        private void OnClick()
        {
            
        }

    }
}