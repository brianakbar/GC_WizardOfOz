using UnityEngine;

namespace Creazen.Wizard.BehaviorTree.Composite {
    public class SelectorNode : CompositeNode {
        int current = 0;

        protected override void OnStart() {
            current = 0;
        }

        protected override State OnUpdate() {
            Node child = children[current];
            if(child.Update() != State.Failure) {
                return child.state;
            }
            return ++current >= children.Count? State.Failure : State.Running;
        }
    }
}
