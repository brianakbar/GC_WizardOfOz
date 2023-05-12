namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;

    public class RootNode : Node {
        [HideInInspector] public Node child;

        protected override State OnUpdate() {
            return child.Update();
        }

        public override Node Clone() {
            RootNode instance = Instantiate(this);
            instance.child = child.Clone();
            return instance;
        }

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