namespace Creazen.Wizard.BehaviorTree.Decorator {
    public class RepeatUntilRunningNode : DecoratorNode {
        protected override State OnUpdate() {
            if(child.Update() != State.Running) {
                return State.Running;
            }
            return State.Success;
        }
    }
}