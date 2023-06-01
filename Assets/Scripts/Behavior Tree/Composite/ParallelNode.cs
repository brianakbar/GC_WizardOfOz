namespace Creazen.Wizard.BehaviorTree.Composite {
    public class ParallelNode : CompositeNode {
        protected override State OnUpdate() {
            foreach(Node child in children) {
                child.Update();
            }
            return State.Running;
        }
    }
}
