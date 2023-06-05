namespace Creazen.Wizard.Event {
    using UnityEngine;
    using UnityEngine.Events;

    public class VoidEventListener : MonoBehaviour {
        [SerializeField] VoidEventChannel channel;
        [SerializeField] UnityEvent onEventRaised;

        void OnEnable() {
            if(channel == null) return;

            channel.onEventRaised += OnEventRaised;
        }

        void OnDisable() {
            if(channel == null) return;

            channel.onEventRaised -= OnEventRaised;
        }

        void OnEventRaised() {
            onEventRaised?.Invoke();
        }
    }
}