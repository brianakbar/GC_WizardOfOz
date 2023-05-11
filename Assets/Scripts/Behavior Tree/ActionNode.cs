namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;

    public abstract class ActionNode : Node {
        public override bool AddChild(Node child) {
            return false;
        }

        public override bool RemoveChild(Node child) {
            return false;
        }

        public override IEnumerable<Node> GetChildren() {
            yield break;
        }
    }
}
