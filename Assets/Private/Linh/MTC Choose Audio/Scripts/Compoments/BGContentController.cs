using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class BGContentController : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        public Image Image { get; set; }
        public void FadeButton(float value)
        {
            canvasGroup.DOFade(value, 0.5f);
        }
        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
        public void SetScale(Vector3 scale, float time)
        {
            transform.DOScale(scale, time).SetEase(Ease.Linear);
        }
    }
}
