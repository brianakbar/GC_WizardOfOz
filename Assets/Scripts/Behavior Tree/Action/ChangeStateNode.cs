namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    public class ChangeStateNode : ActionNode {
        [SerializeField] string to;

        protected override State OnUpdate() {
            behaviorTree.blackboard.state = to;
            return State.Success;
        }
    }
}
