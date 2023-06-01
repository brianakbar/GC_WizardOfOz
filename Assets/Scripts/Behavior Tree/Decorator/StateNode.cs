namespace Creazen.Wizard.BehaviorTree.Decorator {
    using UnityEngine;

    public class StateNode : DecoratorNode {
        [SerializeField] string stateName = "Default";

        protected override State OnUpdate() {
            if(behaviorTree.blackboard.state == stateName) {
                child.Update();
                return State.Running;
            } 
            Abort(State.Failure);
            return State.Failure;
        }
    }
}