namespace Creazen.Wizard.BehaviorTree.Decorator {
    public class RepeaterNode : DecoratorNode {
        protected override State OnUpdate() {
            child.Update();
            return State.Running;
        }
    }
}