namespace Creazen.Wizard.BehaviorTree.Composite {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Sequencer Node", menuName = "Behavior Tree/Composite/Sequencer")]
    public class SequencerNode : CompositeNode {
        int current = 0;

        protected override void OnStart() {
            current = 0;
        }

        protected override State OnUpdate() {
            Node child = children[current++];
            if(child.Update() != State.Success) {
                return child.state;
            }
            return current >= children.Count? State.Success : State.Running;
        }
    }
}
