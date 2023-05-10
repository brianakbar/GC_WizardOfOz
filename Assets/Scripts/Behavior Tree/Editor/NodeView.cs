namespace Creazen.Wizard.BehaviorTree.Editor {
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;

    public class NodeView : Node {
        Creazen.Wizard.BehaviorTree.Node node;

        public NodeView(Creazen.Wizard.BehaviorTree.Node node) {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.guid;

            style.left = node.position.x;
            style.top = node.position.y;
        }

        public Creazen.Wizard.BehaviorTree.Node GetNode() {
            return node;
        }

        public override void SetPosition(Rect newPos) {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }
    }
}