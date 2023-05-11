namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;

    public abstract class DecoratorNode : Node {
        public Node child;

        public override bool AddChild(Node child) {
            this.child = child;

            return true;
        }

        public override bool RemoveChild(Node child) {
            if(this.child != child) return false;
            
            this.child = null;

            return true;
        }

        public override IEnumerable<Node> GetChildren() {
            if(this.child == null) yield break;

            yield return this.child;
        }
    }
}
