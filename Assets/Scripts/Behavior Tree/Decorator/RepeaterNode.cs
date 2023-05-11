namespace Creazen.Wizard.BehaviorTree.Decorator {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Repeat Node", menuName = "Behavior Tree/Decorator/Repeater")]
    public class RepeaterNode : DecoratorNode {
        protected override State OnUpdate() {
            child.Update();
            return State.Running;
        }
    }
}