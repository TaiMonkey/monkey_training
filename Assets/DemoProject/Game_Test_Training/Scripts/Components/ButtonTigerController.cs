using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Monkey.Game.GameTest
{
    public class ButtonTigerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector3 originalScale;
        [SerializeField] SkeletonGraphic skeletonGraphic;
        public int Cage_type = (int)GameTestTypeCage.Cage_Tiger;

        public void SetAnimStart(string anim)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, anim, true);
        }

        public SkeletonGraphic GetSkeletonGraphic()
        {
            return skeletonGraphic;
        }

        public void SetScaleSpine(float scale, bool scaleUp)
        {
            if (scaleUp)
            {
                skeletonGraphic.transform.localScale *= scale;
            }
            else
            {
                skeletonGraphic.transform.localScale = originalScale;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.LogError("OnPointerDown");
            originalScale = skeletonGraphic.transform.localScale;
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Down, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.LogError("OnPointerUp");
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Up, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }
    }
}
