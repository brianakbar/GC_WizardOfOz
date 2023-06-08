namespace Creazen.Wizard.Event.Audio {
    using UnityEngine;
    using UnityEngine.Events;

    [CreateAssetMenu(fileName = "Audio Play Event Channel", menuName = "Event/Audio/Audio Play", order = 0)]
    public class AudioPlayEventChannel : ScriptableObject {
        public UnityAction<AudioSetting> onEventRaised;

        public void RaiseEvent(AudioSetting audioSetting) {
            onEventRaised?.Invoke(audioSetting);
        }
    }
}