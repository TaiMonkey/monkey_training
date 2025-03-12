using Cysharp.Threading.Tasks;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.BreakTheEgg
{
    public class InitState : FSMState
    {
        private InitStateDependency dependency;
        private InitStateData stateData;
        private CancellationTokenSource cts;
        private List<string> listAnimation;
        private List<int> listIndex;

        public override void SetUp(object data)
        {
            dependency = (InitStateDependency)data;
        }

        public override async void OnEnter(object data)
        {
            SoundChannel soundChannel;
            stateData = (InitStateData)data;
            cts = new();
            Debug.LogError("InitState");

            SetUpListAnimation();
            listIndex = new List<int>();
            for (int i = 0; i < dependency.buttonEggControllers.Count; i++)
            {
                listIndex.Add(i);
            }

            for (int i = 0; i < dependency.buttonEggControllers.Count; i++)
            {
                ButtonEggController buttonEgg = dependency.buttonEggControllers[i];
                int index = RandomAnimation();
                buttonEgg.SetAnimation(listAnimation[index], false);
                buttonEgg.SetAlphabet(stateData.ListButtonEggData[i].AlphaBetAnser);
                buttonEgg.SetTextAnswer(stateData.ListButtonEggData[i].TextAnswer);
                buttonEgg.gameObject.SetActive(false);
            }
            InActiveObject(dependency.ListImageBack);
            InActiveObject(dependency.ListImageFront);

            bool isFinishAudioBackground = false;
            soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.InitConfig.Bg_audio, () => { isFinishAudioBackground = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            dependency.LogoMovement.MoveLogoIn();
            await UniTask.Delay(dependency.InitConfig.Delay500, cancellationToken: cts.Token);

            bool isFinishAudioCTA = false;
            soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, dependency.InitConfig.AudioCTA, () => { isFinishAudioCTA = true; });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);
            await UniTask.Delay(dependency.InitConfig.Delay3000, cancellationToken: cts.Token);

            dependency.LogoMovement.MoveLogoOut();
            await UniTask.Delay(dependency.InitConfig.Delay500, cancellationToken: cts.Token);

            StateChanel stateChanel = new StateChanel(StateName.Status.InitFinish);
            ObserverManager.TriggerEvent(stateChanel);

        }

        private void InActiveObject(List<Image> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                list[i].gameObject.SetActive(false);
            }
        }

        public List<string> SetUpListAnimation()
        {
            listAnimation = new List<string>();
            listAnimation.Add(dependency.SkinConfig.EggA.EggNormal);
            listAnimation.Add(dependency.SkinConfig.EggB.EggNormal);
            listAnimation.Add(dependency.SkinConfig.EggC.EggNormal);

            return listAnimation;
        }

        public int RandomAnimation()
        {
            int index = Random.Range(0, listIndex.Count);
            int selectedIndex = listIndex[index];
            listIndex.RemoveAt(index); 
            return selectedIndex; 
        }
    }

    public class InitStateDependency
    {
        public List<ButtonEggController> buttonEggControllers { get; set; }
        public LogoMovement LogoMovement { get; set; }
        public InitConfig InitConfig { get; set; }
        public SkinConfig SkinConfig { get; set; }

        public List<Image> ListImageBack { get; set; }
        public List<Image> ListImageFront { get; set; }
    }

    public class InitStateData
    {
        public List<ButtonEggData> ListButtonEggData { get; set; }
    }

    public class ButtonEggData
    {
        public string AlphaBetAnser { get; set; }
        public string TextAnswer { get; set; }
        public Sprite ImageAnswer { get; set; }
    }
}
