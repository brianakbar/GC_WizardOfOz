namespace Creazen.Wizard.BehaviorTree.Composite {
    public class SequencerNode : CompositeNode {
        int current = 0;

        protected override void OnStart() {
            current = 0;
        }

        protected override State OnUpdate() {
            Node child = children[current];
            if(child.Update() != State.Success) {
                return child.state;
            }
            return ++current >= children.Count? State.Success : State.Running;
        }
    }
}
