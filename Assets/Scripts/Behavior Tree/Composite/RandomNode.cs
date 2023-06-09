namespace Creazen.Wizard.BehaviorTree.Composite {
    using UnityEngine;

    public class RandomNode : CompositeNode {
        int selectedNode = 0;

        protected override void OnStart() {
            selectedNode = Random.Range(0, children.Count);
        }

        protected override State OnUpdate() {
            Node child = children[selectedNode];
            return child.Update();
        }
    }
}
