using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Monkey.Game.CF
{
    public class CFIntroState : FSMState
    {
        private CFIntroStateDependency dependency;
        private CFIntroStateData introData;
        private CancellationTokenSource cts;
        
        public override void SetUp(object data)
        {
            dependency = (CFIntroStateDependency)data;
        }
        public override void OnEnter(object data)
        {
            cts = new();
            introData = (CFIntroStateData)data;

            List<ButtonDomdomCotroller> buttonDomdomCotrollers = dependency.ButtonDomdomCotrollers;
            foreach (var firefly in dependency.ButtonDomdomCotrollers)
            {
                firefly.DomDomMove();
                
            }

            SoundChannel soundChannel = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, introData.AudioClip, () => { });
            ObserverManager.TriggerEvent<SoundChannel>(soundChannel);

            StateChanel stateChanel = new StateChanel(StateName.Status.IntroFinish);
            ObserverManager.TriggerEvent(stateChanel);

        }
    }
    public class CFIntroStateDependency
    {
        public List<ButtonDomdomCotroller> ButtonDomdomCotrollers { get; set; }
        public List<ButtonJarController> ButtonJarControllers { get; set; }
    }
    public class CFIntroStateData
    {
        public AudioClip AudioClip { get; set; }
    }

}
