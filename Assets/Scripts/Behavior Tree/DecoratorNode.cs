namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public abstract class DecoratorNode : Node {
        [SerializeField] protected Node child;

        public override Node Clone() {
            DecoratorNode instance = Instantiate(this);
            instance.child = child.Clone();
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
