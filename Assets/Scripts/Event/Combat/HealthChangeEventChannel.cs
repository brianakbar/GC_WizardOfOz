namespace Creazen.Wizard.Event.Combat {
    using UnityEngine;
    using UnityEngine.Events;

    [CreateAssetMenu(fileName = "Health Change Event Channel", menuName = "Event/Combat/Health Change", order = 0)]
    public class HealthChangeEventChannel : ScriptableObject {
        public UnityAction<float> onEventRaised;

        public void RaiseEvent(float healthFraction) {
            onEventRaised?.Invoke(healthFraction);
        }
    }
}