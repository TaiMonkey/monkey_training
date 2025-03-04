using DG.Tweening;
using MonkeyBase.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Monkey.MJ5.BESTW01SortTheWords
{
    public class BESTW01HandleData
    {
        public static float SCENCE_RESOLUTION = 1.77f;
        public static int MAX_TURN_DRAG = 10;
        public static int MAX_TURN_BOX = 5;
        public const float MAX_DISTANCE = 3f;
        public static int CurrentTurnBoxLeft { get; set; } = 0;
        public static int CurrentTurnBoxRight { get; set; } = 0;

        public static void TriggerFinishState(BESTW01State state, object data)
        {
            BESTW01DataChanner dataChanner = new BESTW01DataChanner(state, data);
            ObserverManager.TriggerEvent(dataChanner);
        }

        public static void TriggerStateInput(BESTW01UserInput input, object data)
        {
            BESTW01InputChanner buttonData = new BESTW01InputChanner(input, data);
            ObserverManager.TriggerEvent(buttonData);
        }
        public static void TriggerStateCarousel(BESTW01UserInput input, object data)
        {
            BESTW01CarouselChanner buttonData = new BESTW01CarouselChanner(input, data);
            ObserverManager.TriggerEvent(buttonData);
        }
        public static bool CheckAlphaItem(List<BESTW01CardItem> list)
        {
            foreach (var item in list)
            {
                if(item.GetComponent<CanvasGroup>().alpha != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void EnableCards(List<BESTW01CardItem> list, bool isEnable)
        {
            foreach (var item in list)
            {
                if (!item.IsDragged) item.Enable(isEnable);
            }
        }
        public static void EnableCards(List<BESTW01CardItem> list, int idCard, bool isEnable)
        {
            foreach (var item in list)
            {
                if (!AreIntegersEqual(item.IdCard, idCard) && !item.IsDragged) item.Enable(isEnable);
            }
        }

        public static bool AreIntegersEqual(int a, int b)
        {
            return a == b;
        }
        public static void SetAnimation(SkeletonGraphic animation, string input, bool isLoop, Spine.AnimationState.TrackEntryDelegate onCompleteCallback)
        {
            animation.AnimationState.SetAnimation(0, input, isLoop).Complete += onCompleteCallback;
        }

        public static void SetAnimation(SkeletonGraphic animation, string input, int numberBox, int numberLever, bool isLoop, Spine.AnimationState.TrackEntryDelegate onCompleteCallback)
        {
            string inputReplace = ReplaceTwoNumbers(input, numberBox, numberLever);
            animation.AnimationState.SetAnimation(0, inputReplace, isLoop).Complete += onCompleteCallback;
        }
        public static void SetAnimation(SkeletonGraphic animation, string input, int numberBox, bool isLoop, Spine.AnimationState.TrackEntryDelegate onCompleteCallback)
        {
            string pattern = @"(\d)";
            string replacement = numberBox.ToString();
            string result = Regex.Replace(input, pattern, replacement);
            animation.AnimationState.SetAnimation(0, result, isLoop).Complete += onCompleteCallback;
        }

        public static float MilisecondsToSeconds(int value)
        {
            return (float)value / 1000;
        }
        public static string ReplaceTwoNumbers(string input, int newNumber1, int newNumber2)
        {
            var match = Regex.Matches(input, @"\d+");

            if (match.Count >= 2)
            {
                string result = Regex.Replace(input, @"\d+", m =>
                {
                    if (m.Index == match[0].Index) return newNumber1.ToString();
                    if (m.Index == match[1].Index) return newNumber2.ToString();
                    return m.Value; 
                });

                return result;
            }

            return input;
        }
        public static void KillTweening(List<Transform> lists)
        {
            foreach (Transform child in lists)
            {
                child.DOKill();
            }
        }
        public static void KillTweening(List<BESTW01CardItem> lists)
        {
            foreach (BESTW01CardItem child in lists)
            {
                child.transform.DOKill();
            }
        }
        public static void DestroyItem(Transform parent)
        {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        public static BESTW01CardItem FindNearestTransform(List<BESTW01CardItem> listCard, Transform pointCenter, bool checkAlpha)
        {
            if (listCard == null || listCard.Count == 0 || pointCenter == null)
                return null;

            BESTW01CardItem nearestTransform = null;
            float shortestDistance = Mathf.Infinity;

            foreach (var t in listCard)
            {
                if (checkAlpha)
                {
                    CanvasGroup canvasGroup = t.GetComponent<CanvasGroup>();
                    if (canvasGroup != null && canvasGroup.alpha != 0)
                    {
                        float distance = Vector3.Distance(t.transform.position, pointCenter.position);
                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            nearestTransform = t;
                        }
                    }
                }else
                {
                    float distance = Vector3.Distance(t.transform.position, pointCenter.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestTransform = t;
                    }
                }
            }

            return nearestTransform;
        }


        public static bool IsUIElementVisible(Camera camera, RectTransform uiElement)
        {
            Vector3[] corners = new Vector3[4];
            uiElement.GetWorldCorners(corners);

            foreach (Vector3 corner in corners)
            {
                Vector3 viewportPosition = camera.WorldToViewportPoint(corner);

                if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
                    viewportPosition.y < 0 || viewportPosition.y > 1 ||
                    viewportPosition.z <= 0) 
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckTriggerOfTwoObject(RectTransform objectA, RectTransform objectB, float percent)
        {
            Vector3[] objectACorners = new Vector3[4];
            Vector3[] objectBCorners = new Vector3[4];

            objectA.GetWorldCorners(objectACorners);
            objectB.GetWorldCorners(objectBCorners);

            Rect rectA = new(objectACorners[0].x, objectACorners[0].y, objectACorners[2].x - objectACorners[0].x, objectACorners[2].y - objectACorners[0].y);

            Rect rectB = new(objectBCorners[0].x, objectBCorners[0].y, objectBCorners[2].x - objectBCorners[0].x, objectBCorners[2].y - objectBCorners[0].y);

            Rect intersection = Rect.MinMaxRect(
                Mathf.Max(rectA.xMin, rectB.xMin),
                Mathf.Max(rectA.yMin, rectB.yMin),
                Mathf.Min(rectA.xMax, rectB.xMax),
                Mathf.Min(rectA.yMax, rectB.yMax)
            );

            float intersectionArea = Mathf.Max(0, intersection.width) * Mathf.Max(0, intersection.height);
            float rectAArea = rectA.width * rectA.height;
            float rectBArea = rectB.width * rectB.height;

            float overlapPercentageA = (intersectionArea / rectAArea) * 100f;
            float overlapPercentageB = (intersectionArea / rectBArea) * 100f;

            return overlapPercentageA >= percent * 100f && overlapPercentageB >= percent * 100f;
        }
    }
}