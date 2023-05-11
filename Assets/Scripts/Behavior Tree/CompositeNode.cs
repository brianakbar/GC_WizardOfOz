namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;

    public abstract class CompositeNode : Node {
        public List<Node> children = new List<Node>();

        public override bool AddChild(Node child) {
            if(child == null) return false;
            if(children.Contains(child)) return false;

            children.Add(child);
            return true;
        }

        public override bool RemoveChild(Node child) {
            if(child == null) return false;
            if(!children.Contains(child)) return false;

            children.Remove(child);
            return true;
        }

        public override IEnumerable<Node> GetChildren() {
            return children;
        }
    }
}