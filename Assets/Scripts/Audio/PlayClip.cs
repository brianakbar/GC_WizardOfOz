namespace Creazen.Wizard.Audio {
    using Creazen.Wizard.Event.Audio;
    using UnityEngine;
    
    public class PlayClip : MonoBehaviour {
        [SerializeField] bool playAtStart = false;
        [SerializeField] AudioClip clip;
        [SerializeField] bool isOneShot = false;

        [Header("Channels")]
        [SerializeField] AudioPlayEventChannel audioPlayChannel;

        void Start() {
            if(!playAtStart) return;
            
            Play();
        }

        public void Play() {
            if(clip == null) return;

            audioPlayChannel.RaiseEvent(new AudioSetting() {
                Clip = clip,
                IsOneShot = isOneShot
            });
        }
    }
}