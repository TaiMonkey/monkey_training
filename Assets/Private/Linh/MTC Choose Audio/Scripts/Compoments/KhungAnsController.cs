using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monkey.Game.MTCCA
{
    public class KhungAnsController : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        public RectTransform Rect { get; set; }
        public void SetScale(Vector3 scale, float time)
        {
            transform.DOScale(scale, time).SetEase(Ease.Linear);
        }
        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
    }

}
