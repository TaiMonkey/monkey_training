using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Box : BaseButton
    {
        [SerializeField] private SkeletonGraphic skeletonBox;
        [SerializeField] private SkeletonGraphic fireWork;
        [SerializeField] private BaseText textData;
        private BESTW01CardData data;
        private bool isPlaying = false;

        public bool IsPlaying
        {
            set { isPlaying = value; }
            get { return isPlaying; }
        }     
        public void InitData(BESTW01CardData data)
        {
            this.data = data;
            textData.SetText(data.text);

        }
        public BESTW01CardData GetData()
        {
            return data;
        }
        public SkeletonGraphic GetSkeleton()
        {
            return skeletonBox;
        }
        public SkeletonGraphic GetFirework()
        {
            return fireWork;
        }

        public override void Enable(bool isEnable)
        {
            button.interactable = isEnable;
        }

        public override void OnClick()
        {
            BESTW01HandleData.TriggerStateInput(BESTW01UserInput.ClickBox, gameObject);
        }
    }
}