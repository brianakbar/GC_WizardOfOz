namespace Creazen.Wizard.BehaviorTree {
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Behavior Tree", menuName = "Behavior Tree/Behavior Tree", order = 0)]
    public class BehaviorTree : ScriptableObject {
        public RootNode rootNode;
        public State state = State.Running;
        
        [SerializeField] List<Node> nodes = new List<Node>();

        public State Update() {
            if(rootNode.state == State.Running) {
                state = rootNode.Update();
            }
            return state;
        }

        public IEnumerable<Node> GetNodes() {
            return nodes;
        }

        public IEnumerable<Node> GetChildren() {
            return GetChildren(rootNode);
        }

        public IEnumerable<Node> GetChildren(Node parent) {
            return parent.GetChildren();
        }

        public void Traverse(Node node, Action<Node> visiter) {
            if(!node) return;

            visiter.Invoke(node);
            foreach(Node child in node.GetChildren()) {
                Traverse(child, visiter);
            }
        }

        public BehaviorTree Clone() {
            BehaviorTree clonedTree = Instantiate(this);
            clonedTree.rootNode = rootNode.Clone() as RootNode;
            clonedTree.nodes = new List<Node>();
            Traverse(clonedTree.rootNode, (node) => {
                clonedTree.nodes.Add(node);
            });

            return clonedTree;
        }

#if UNITY_EDITOR
        public Node CreateNode(System.Type type) {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = Guid.NewGuid().ToString();
            Undo.RegisterCreatedObjectUndo(node, "Create Node");

            Undo.RecordObject(this, "Add Node");
            nodes.Add(node);
            EditorUtility.SetDirty(this);

            if(!Application.isPlaying) {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node nodeToDelete) {
            if(!nodes.Contains(nodeToDelete)) return;
            if(nodeToDelete is RootNode) return;

            Undo.RecordObject(this, "Delete Node");
            nodes.Remove(nodeToDelete);
            EditorUtility.SetDirty(this);

            //AssetDatabase.RemoveObjectFromAsset(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
            AssetDatabase.SaveAssets();
        }

        public bool AddChild(Node parent, Node child) {
            return parent.AddChild(child);
        }

        public bool RemoveChild(Node parent, Node child) {
            return parent.RemoveChild(child);
        }
#endif

    }
}
