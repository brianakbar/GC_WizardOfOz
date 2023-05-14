namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public abstract class CompositeNode : Node {
        [SerializeField] protected List<Node> children = new List<Node>();

        public override Node Clone() {
            CompositeNode instance = Instantiate(this);
            instance.children = children.ConvertAll((child) => child.Clone());
            return instance;
        }

        public override IEnumerable<Node> GetChildren() {
            return children;
        }

#if UNITY_EDITOR
        public override bool AddChild(Node child) {
            if(child == null) return false;
            if(children.Contains(child)) return false;

            Undo.RecordObject(this, "Add Node Child");
            children.Add(child);
            EditorUtility.SetDirty(this);

            return true;
        }

        public override bool RemoveChild(Node child) {
            if(child == null) return false;
            if(!children.Contains(child)) return false;

            Undo.RecordObject(this, "Remove Node Child");
            children.Remove(child);
            EditorUtility.SetDirty(this);

            return true;
        }

        public void SortChildren() {
            children.Sort(SortByHorizontalPosition);
        }

        int SortByHorizontalPosition(Node left, Node right) {
            return left.GetPosition().x < right.GetPosition().x ? -1 : 1;
        }
#endif

    }
}