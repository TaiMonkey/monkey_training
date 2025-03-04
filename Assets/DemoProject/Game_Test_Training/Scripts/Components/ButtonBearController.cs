using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Monkey.Game.GameTest
{
    public class ButtonBearController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] SkeletonGraphic skeletonGraphic;
        private Vector3 originalScale;
        public int Cage_type = (int)GameTestTypeCage.Cage_Bear;

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
            originalScale = skeletonGraphic.transform.localScale;
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Down, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Pointer_Up, this);
            ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
        }
    }
}

