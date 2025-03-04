using Cysharp.Threading.Tasks;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01InitState : FSMState
    {
        private BESTW01InitStateObjectDependency dependency;
        private CancellationTokenSource cts;

        public override void OnEnter(object data)
        {
            base.OnEnter(data);
            BESTW01InitStateData initStateData = (BESTW01InitStateData)data;
            DoWork(initStateData);
        }

        public override void SetUp(object data)
        {
            dependency = (BESTW01InitStateObjectDependency)data;
        }

        private void DoWork(BESTW01InitStateData initData)
        {
            cts = new CancellationTokenSource();
            //GameHelper.Instance.EnableTouch();
            //GameHelper.SetMultiTouchEnabled(false);

            if (initData.AspectRatio > BESTW01HandleData.SCENCE_RESOLUTION)
            {
                SetAnchorUIConveyor(dependency.PointAnchorConveyorPhone);
                SetAnchorUICarousel(dependency.PointAnchorCarousePhone);
            }
            else
            {
                SetAnchorUIConveyor(dependency.PointAnchorConveyorTablet);
                SetAnchorUICarousel(dependency.PointAnchorCarouseTablet);
            }
            dependency.BoxLeft.InitData(initData.DataPlay.cardDataLeft);
            dependency.BoxLeft.Enable(false);
            dependency.BoxLeft.IsPlaying = false;

            dependency.BoxRight.InitData(initData.DataPlay.cardDataRight);
            dependency.BoxRight.Enable(false);
            dependency.BoxRight.IsPlaying = false;


            List<BESTW01Box> boxes = new List<BESTW01Box>();
            boxes.Add(dependency.BoxLeft);
            boxes.Add(dependency.BoxRight);

            BESTW01HandleData.SetAnimation(dependency.BoxLeft.GetSkeleton(), dependency.BoxConfig.boxNormal, (int)dependency.BoxLeft.GetData().typeBox, initData.CurrentTurnBoxLeft, false, null);
            BESTW01HandleData.SetAnimation(dependency.BoxRight.GetSkeleton(), dependency.BoxConfig.boxNormal, (int)dependency.BoxRight.GetData().typeBox, initData.CurrentTurnBoxRight, false, null);

            List<BESTW01CardData> dataPool = new List<BESTW01CardData>();

            for (int i = 0; i < 5; i++)
            {
                dataPool.Add(initData.DataPlay.cardDataLeft);
                dataPool.Add(initData.DataPlay.cardDataRight);
            }

            List<BESTW01CardData> shuffledList = ShuffleWithCondition(dataPool);


            for (int i = 0; i < dependency.CardItems.Count; i++)
            {
                int indexPoint = i % 6;
                dependency.CardItems[i].InitData(shuffledList[i], i, indexPoint, boxes, dependency.Carousel, dependency.Guiding, 
                    dependency.CardConfig, dependency.BoxConfig, dependency.DragResultConfig);
                dependency.CardItems[i].Enable(false);
            }

            /*  SkeletonData skeletonData = dependency.CardItems[0].GetSkeleton().SkeletonData;

              for (int i = 0; i < skeletonData.Animations.Count; i++)
              {
                  string animationName = skeletonData.Animations.Items[i].Name;
                  Debug.Log("Animation State: " + animationName);
              }*/

            dependency.Carousel.InitData(dependency.CardItems, dependency.PointCardItems, dependency.ConveyorItems);
            dependency.Guiding.InitData(dependency.GuidingConfig, boxes, dependency.Carousel);
            BESTW01HandleData.TriggerFinishState(BESTW01State.IntroGame, null);
        }
        private List<BESTW01CardData> ShuffleWithCondition(List<BESTW01CardData> dataPool)
        {
            System.Random rand = new System.Random();
            List<BESTW01CardData> shuffled = new List<BESTW01CardData>();
            bool validShuffle = false;

            while (!validShuffle)
            {
                shuffled = dataPool.OrderBy(x => rand.Next()).ToList();
                validShuffle = CheckCondition(shuffled);
            }

            return shuffled;
        }

        private static bool CheckCondition(List<BESTW01CardData> list)
        {
            int countLeft = 0;
            int countRight = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].typeBox == BESTW01TypeBox.Left)
                {
                    countLeft++;
                    countRight = 0; // Reset đếm bên phải
                }
                else
                {
                    countRight++;
                    countLeft = 0; // Reset đếm bên trái
                }

                // Kiểm tra điều kiện
                if (countLeft >= 4 || countRight >= 4)
                {
                    return false; // Không hợp lệ
                }
            }

            return true; // Hợp lệ
        }

        private void SetAnchorUIConveyor(Transform pointAnchor) {
            RectTransform rectConveyor = dependency.UiConveyor.GetComponent<RectTransform>();
            RectTransform rectPoint = pointAnchor.GetComponent<RectTransform>();

            rectConveyor.anchoredPosition = new Vector2(rectConveyor.anchoredPosition.x, rectPoint.anchoredPosition.y);
            dependency.UiConveyor.position = new Vector3(dependency.UiConveyor.position.x, pointAnchor.position.y, dependency.UiConveyor.position.z);

        }
        private void SetAnchorUICarousel(Transform pointAnchor)
        {
            RectTransform rectPoint = pointAnchor.GetComponent<RectTransform>();

            RectTransform rectCarousel = dependency.UICarouselCard.GetComponent<RectTransform>();
            RectTransform rectPointCard = dependency.UIPointCard.GetComponent<RectTransform>();

            rectCarousel.anchoredPosition = new Vector2(rectCarousel.anchoredPosition.x, rectPoint.anchoredPosition.y);
            rectPointCard.anchoredPosition = new Vector2(rectPointCard.anchoredPosition.x, rectPoint.anchoredPosition.y);

            dependency.UICarouselCard.position = new Vector3(dependency.UICarouselCard.position.x, pointAnchor.position.y, dependency.UICarouselCard.position.z);
            dependency.UIPointCard.position = new Vector3(dependency.UIPointCard.position.x, pointAnchor.position.y, dependency.UIPointCard.position.z);
        }

        public override void OnExit()
        {
            base.OnExit();
            cts?.Cancel();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Cancel();
            cts?.Dispose();
        }
    }
    public class BESTW01InitStateData
    {
        public BESTW01GamePlayData DataPlay { get; set; }
        public float AspectRatio { get; set; }
        public int CurrentTurnBoxLeft { get; set; }
        public int CurrentTurnBoxRight { get; set; }
    }

    public class BESTW01InitStateObjectDependency
    {
        public BESTW01CardConfig CardConfig { get; set; }
        public BESTW01BoxConfig BoxConfig { get; set; }
        public BESTW01SDragResultConfig DragResultConfig { get; set; }
        public BESTW01GuidingConfig GuidingConfig { get; set; }
        public BESTW01Guiding Guiding { get; set; }
        public BESTW01Box BoxLeft { get; set; }
        public BESTW01Box BoxRight { get; set; }
        public BESTW01Carousel Carousel { get; set; }
        public List<BESTW01CardItem> CardItems { get; set; }
        public List<Transform> PointCardItems { get; set; }
        public List<Transform> ConveyorItems { get; set; }
        public Camera CameraGame { get; set; }
        public Transform UiConveyor { get; set; }
        public Transform UICarouselCard { get; set; }
        public Transform UIPointCard { get; set; }
        public Transform PointAnchorConveyorPhone { get; set; }
        public Transform PointAnchorConveyorTablet { get; set; }
        public Transform PointAnchorCarousePhone { get; set; }
        public Transform PointAnchorCarouseTablet { get; set; }


    }
}