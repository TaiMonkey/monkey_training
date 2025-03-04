using Coffee.UIExtensions;
using MonkeyBase.Observer;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01Dependency : Dependency
    {
        [Header("CONFIGURATION")]
        [SerializeField] private BESTW01ConfigSO configSO;
        [SerializeField] BESTW01Guiding guiding;
        [SerializeField] Camera cameraGame;
        [SerializeField] private UIParticle uIParticle;
        [SerializeField] private ParticleSystem effectEndGame;
        [SerializeField] private Transform uiConveyor;
        [SerializeField] private Transform uICarouselCard;
        [SerializeField] private Transform uIPointCard;
        [SerializeField] private Transform pointAnchorConveyorPhone;
        [SerializeField] private Transform pointAnchorConveyorTablet;
        [SerializeField] private Transform pointAnchorCarousePhone;
        [SerializeField] private Transform pointAnchorCarouseTablet;
        [Header("CONTENT")]
        [SerializeField] private BESTW01Box boxLeft;
        [SerializeField] private BESTW01Box boxRight;
        [SerializeField] private Transform pointStart;
        [SerializeField] private Transform pointEnd;
        [SerializeField] private BESTW01Carousel carousel;
        [SerializeField] private List<BESTW01CardItem> cardItems;
        [SerializeField] private List<Transform> pointCardItems;
        [SerializeField] private List<Transform> conveyorItems;

        private void Start()
        {
            SoundChannel bgMusic = new(SoundChannel.PLAY_MUSIC, configSO.audioBackground, null, 1f, true);
            ObserverManager.TriggerEvent<SoundChannel>(bgMusic);
        }
        public void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                SoundChannel bgMusic = new(SoundChannel.PLAY_MUSIC, configSO.audioBackground, null, 1f, true);
                ObserverManager.TriggerEvent<SoundChannel>(bgMusic);
            }
        }

        public override T GetStateData<T>()
        {
            T data; 

            Type listType = typeof(T);
            if (listType == typeof(BESTW01InitStateObjectDependency))
            {
                BESTW01InitStateObjectDependency initData = new BESTW01InitStateObjectDependency();
                initData.CardConfig = configSO.cardConfig;
                initData.DragResultConfig = configSO.dragResultConfig;
                initData.BoxConfig = configSO.boxConfig;
                initData.GuidingConfig = configSO.guidingConfig;
                initData.Guiding = guiding;
                initData.BoxLeft = boxLeft;
                initData.BoxRight = boxRight;
                initData.CardItems = cardItems;
                initData.Carousel = carousel;
                initData.PointCardItems = pointCardItems;
                initData.ConveyorItems = conveyorItems;
                initData.CameraGame = cameraGame;
                initData.UICarouselCard = uICarouselCard;
                initData.UiConveyor = uiConveyor;
                initData.UIPointCard = uIPointCard;
                initData.PointAnchorCarousePhone = pointAnchorCarousePhone;
                initData.PointAnchorCarouseTablet = pointAnchorCarouseTablet;
                initData.PointAnchorConveyorPhone = pointAnchorConveyorPhone;
                initData.PointAnchorConveyorTablet = pointAnchorConveyorTablet;

                 data = ConvertToType<T>(initData);
            }
            else if (listType == typeof(BESTW01IntroStateObjectDependency))
            {
                BESTW01IntroStateObjectDependency introData = new BESTW01IntroStateObjectDependency();
                introData.IntroConfig = configSO.introConfig;
                introData.Carousel = carousel;
                introData.CardItems = cardItems;

                data = ConvertToType<T>(introData);
            }
            else if (listType == typeof(BESTW01PlayStateObjectDependency))
            {
                BESTW01PlayStateObjectDependency playData = new BESTW01PlayStateObjectDependency();
                playData.BoxConfig = configSO.boxConfig;
                playData.Guiding = guiding;
                playData.CardItems = cardItems;
                playData.BoxLeft = boxLeft;
                playData.BoxRight = boxRight;

                data = ConvertToType<T>(playData);
            }
            else if (listType == typeof(BESTW01ClickStateObjectDependency))
            {
                BESTW01ClickStateObjectDependency clickData = new BESTW01ClickStateObjectDependency();
                clickData.BoxConfig = configSO.boxConfig;
                clickData.Guiding = guiding;
                clickData.CardItems = cardItems;
                clickData.BoxLeft = boxLeft;
                clickData.BoxRight = boxRight;
                data = ConvertToType<T>(clickData);
            }
            else if (listType == typeof(BESTW01DraggingStateObjectDependency))
            {
                BESTW01DraggingStateObjectDependency draggingData = new BESTW01DraggingStateObjectDependency();
                draggingData.CardItems = cardItems;
                draggingData.BoxLeft = boxLeft;
                draggingData.BoxRight = boxRight;
                data = ConvertToType<T>(draggingData);
            }
            /*else if (listType == typeof(BESTW01DragResultStateObjectDependency))
            {
                BESTW01DragResultStateObjectDependency dragResultData = new BESTW01DragResultStateObjectDependency();
                dragResultData.BoxConfig = configSO.boxConfig;
                dragResultData.DragResultConfig = configSO.dragResultConfig;
                dragResultData.CardConfig = configSO.cardConfig;
                dragResultData.CardItems = cardItems;
                dragResultData.BoxLeft = boxLeft;
                dragResultData.BoxRight = boxRight;
                 data = ConvertToType<T>(dragResultData);
            }*/
            else if (listType == typeof(BESTW01EndStateObjectDependency))
            {
                BESTW01EndStateObjectDependency endGameData = new BESTW01EndStateObjectDependency();
                endGameData.EndGameConfig = configSO.endGameConfig;
                endGameData.UIParticle = uIParticle;
                endGameData.EffectEndGame = effectEndGame;
                data = ConvertToType<T>(endGameData);
            }
            else
            {
                data = ConvertToType<T>(null);
            }
            return data;
        }
    }
}