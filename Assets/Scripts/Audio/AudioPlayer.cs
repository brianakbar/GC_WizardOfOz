namespace Creazen.Wizard.Audio {
    using Creazen.Wizard.Event.Audio;
    using UnityEngine;
    
    public class AudioPlayer : MonoBehaviour {
        AudioSource audioSource;

        [Header("Listening on channels")]
        [SerializeField] AudioPlayEventChannel audioPlayChannel;

        void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        void OnEnable() {
            audioPlayChannel.onEventRaised += PlayAudio;
        }

        void OnDisable() {
            audioPlayChannel.onEventRaised -= PlayAudio;
        }

        public void PlayAudio(AudioSetting audioSetting) {
            if(audioSetting.IsOneShot) {
                PlayOneShot(audioSetting);
            }
            else {
                Play(audioSetting);
            }
        }

        void Play(AudioSetting audioSetting) {
            if(audioSource.clip == audioSetting.Clip) return;

            audioSource.clip = audioSetting.Clip;
            audioSource.Play();
        }

        void PlayOneShot(AudioSetting audioSetting) {
            audioSource.PlayOneShot(audioSetting.Clip);
        }
    }
}