namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class RootNode : Node {
        [SerializeField] protected Node child;

        protected override State OnUpdate() {
            return child.Update();
        }

        public override Node Clone() {
            RootNode instance = Instantiate(this);
            if(child != null) instance.child = child.Clone();
            return instance;
        }

        public override IEnumerable<Node> GetChildren() {
            if(this.child == null) yield break;

            yield return this.child;
        }

#if UNITY_EDITOR
        public override bool AddChild(Node child) {
            Undo.RecordObject(this, "Add Node Child");
            this.child = child;
            EditorUtility.SetDirty(this);

            return true;
        }

        public override bool RemoveChild(Node child) {
            if(this.child != child) return false;
            
            Undo.RecordObject(this, "Remove Node Child");
            this.child = null;
            EditorUtility.SetDirty(this);

            return true;
        }
#endif

    }
}
