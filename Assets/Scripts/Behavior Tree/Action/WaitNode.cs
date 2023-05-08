namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    public class WaitNode : ActionNode {
        [SerializeField] float duration;
        float startTime;

        protected override void OnStart() {
            startTime = Time.time;
        }

        protected override State OnUpdate() {
            if(Time.time - startTime >= duration) {
                return State.Success;
            }
            return State.Running;
        }
    }
}
