namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class CompositeNode : Node {
        [HideInInspector] public List<Node> children = new List<Node>();

        public override Node Clone() {
            CompositeNode instance = Instantiate(this);
            instance.children = children.ConvertAll((child) => child.Clone());
            return instance;
        }

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