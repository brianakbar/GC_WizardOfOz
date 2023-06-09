namespace Creazen.Wizard.BehaviorTree.Composite {
    using UnityEngine;

    public class RandomNode : CompositeNode {
        protected override State OnUpdate() {
            Node child = children[Random.Range(0, children.Count)];
            return child.Update();
        }
    }
}
