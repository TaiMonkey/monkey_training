using Cysharp.Threading.Tasks;
using DG.Tweening;
using MonkeyBase.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.GameTest
{
    public class GameTestGuidingState : FSMState
    {
        private GameTestGuidingStateDependency dependency;
        private List<AnimalButtonController> listCurrentEnableActive;
        private AnimalButtonController guidingAnimal;
        private bool isGuiding = false;
        private CancellationTokenSource cts;
        private const int TIME_DELAY = 500;

        public override void SetUp(object data)
        {
            dependency = (GameTestGuidingStateDependency)data;
        }

        public override void OnEnter()
        {
            cts = new CancellationTokenSource();
            listCurrentEnableActive = new List<AnimalButtonController>();
            listCurrentEnableActive = new List<AnimalButtonController>();
            base.OnEnter();
            for(int i = 0; i < dependency.ButtonBearControllers.Count; i ++)
            {
                if(dependency.ButtonBearControllers[i].IsEnable)
                {
                    listCurrentEnableActive.Add(dependency.ButtonBearControllers[i]);
                }
            }
            for (int i = 0; i < dependency.ButtonTigerControllers.Count; i++)
            {
                if (dependency.ButtonTigerControllers[i].IsEnable)
                {
                    listCurrentEnableActive.Add(dependency.ButtonTigerControllers[i]);
                }
            }

            int randomIndex = UnityEngine.Random.Range(0, listCurrentEnableActive.Count);
            /*for(int i = 0; i < listCurrentEnableActive.Count; i++) {
                listCurrentEnableActive[i].IsEnable = false;
            }*/

            StartGuidingDrag(randomIndex);
        }

        public async void StartGuidingDrag(int randomIndex)
        {
            ResetGuidingDrag();
            isGuiding = true;
            SoundChannel soundData;
            try
            {
                dependency.UiGuiding.transform.localScale = Vector3.one;
                await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);
                if (guidingAnimal == null)
                {
                    guidingAnimal = GameObject.Instantiate(listCurrentEnableActive[randomIndex], dependency.Animal, false);
                    guidingAnimal.name = "AnimlGuiding";
                    guidingAnimal.transform.localPosition = Vector3.zero;
                    guidingAnimal.IsEnable = false;
                    guidingAnimal.transform.SetAsLastSibling();
                    guidingAnimal.CanvasGroup.alpha = 0f;
                }
                bool tscFadeDone = false;
                bool tscMoveDone = false;
                while (isGuiding)
                {
                    dependency.UiGuiding.transform.position = listCurrentEnableActive[randomIndex].transform.position;
                    if (guidingAnimal != null) guidingAnimal.transform.position
                                = listCurrentEnableActive[randomIndex].transform.position;

                    dependency.UiGuiding.DOFade(1, 0.2f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };

                    if (guidingAnimal != null) guidingAnimal.CanvasGroup.DOFade(0.5f, 0.2f).SetEase(Ease.Linear);
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);
                    tscFadeDone = false;

                    soundData = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.AnimalButtonConfig.SfxChoose);
                    ObserverManager.TriggerEvent<SoundChannel>(soundData);

                    SetColorImage(dependency.HandLong, 1f);
                    await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);
                    if (guidingAnimal != null) guidingAnimal.transform.DOMove(listCurrentEnableActive[randomIndex].cageTranform.position, 0.5f).SetEase(Ease.Linear);

                    dependency.UiGuiding.transform.DOMove(listCurrentEnableActive[randomIndex].cageTranform.position, 0.5f).SetEase(Ease.Linear).onComplete += () =>
                    {
                        tscMoveDone = true;
                    };
                    await UniTask.WaitUntil(() => tscMoveDone, cancellationToken: cts.Token);
                    tscMoveDone = false;
                    await UniTask.Delay(TIME_DELAY, cancellationToken: cts.Token);

                    dependency.UiGuiding.DOFade(0, 0.35f).SetEase(Ease.Linear).onComplete += () => { tscFadeDone = true; };
                    if (guidingAnimal != null) guidingAnimal.CanvasGroup.DOFade(0, 0.35f).SetEase(Ease.Linear);
                    await UniTask.WaitUntil(() => tscFadeDone, cancellationToken: cts.Token);

                    StateChanel stateChanel = new StateChanel(StateName.Status.GuidingFinish, listCurrentEnableActive);
                    ObserverManager.TriggerEvent(stateChanel);
                }
            }
            catch (OperationCanceledException ex)
            {
                LogMe.Log("Dong ex: " + ex);
            }
        }

        public void ResetGuidingDrag()
        {
            SoundManager.Instance.StopFx();
            if (guidingAnimal != null)
            {

                guidingAnimal.CanvasGroup.DOFade(0, 0.1f).onComplete += () =>
                {
                    guidingAnimal = null;
                    GameObject.Destroy(guidingAnimal);
                    DestroyItem(dependency.Animal.transform);
                };
            }
            isGuiding = false;
            dependency.UiGuiding.DOKill();
            dependency.UiGuiding.transform.localScale = Vector3.zero;
            dependency.UiGuiding.DOFade(0f, 0.1f);
        }

        private void DestroyItem(Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Equals("AnimlGuiding")) GameObject.Destroy(child.gameObject);
            }
        }

        private void SetColorImage(Image image, float indexColor)
        {
            Color currentColor = image.color;
            currentColor.a = indexColor;
            image.color = currentColor;
        }
        public override void OnExit()
        {
            base.OnExit();
            ResetGuidingDrag();
            cts?.Cancel();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            cts?.Dispose();
            cts?.Cancel();
        }
    }

    public class GameTestGuidingStateDependency
    {
        public Image HandLong { get; set; }
        public List<AnimalButtonController> ButtonBearControllers { get; set; }
        public List<AnimalButtonController> ButtonTigerControllers { get; set; }
        public CanvasGroup UiGuiding { get; set; }
        public Transform Animal { get; set; }
        public AnimalButtonConfig AnimalButtonConfig { get; set; }
    }
}
