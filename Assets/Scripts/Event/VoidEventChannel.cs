namespace Creazen.Wizard.Event {
    using UnityEngine;
    using UnityEngine.Events;

    [CreateAssetMenu(fileName = "Void Event Channel", menuName = "Event/Void", order = 0)]
    public class VoidEventChannel : ScriptableObject {
        public UnityAction onEventRaised;

        public void RaiseEvent() {
            onEventRaised?.Invoke();
        }
    }
}