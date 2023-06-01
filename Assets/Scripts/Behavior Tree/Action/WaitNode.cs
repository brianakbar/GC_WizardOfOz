namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;

    public class WaitNode : ActionNode {
        [SerializeField] float minDuration;
        [SerializeField] float maxDuration;
        float startTime;
        float duration;

        protected override void OnStart() {
            startTime = Time.time;
            duration = GetDuration();
        }

        protected override State OnUpdate() {
            if(Time.time - startTime >= duration) {
                return State.Success;
            }
            return State.Running;
        }

        float GetDuration() {
            if(minDuration >= maxDuration) {
                return minDuration;
            }
            return Random.Range(minDuration, maxDuration);
        }
    }
}
