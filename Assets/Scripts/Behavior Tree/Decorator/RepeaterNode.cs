namespace Creazen.Wizard.BehaviorTree.Decorator {
    using UnityEngine;

    public class RepeaterNode : DecoratorNode {
        protected override State OnUpdate() {
            child.Update();
            return State.Running;
        }
    }
}