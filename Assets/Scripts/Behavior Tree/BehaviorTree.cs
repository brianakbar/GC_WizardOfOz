namespace Creazen.Wizard.BehaviorTree {
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Behavior Tree", menuName = "Behavior Tree/Behavior Tree", order = 0)]
    public class BehaviorTree : ScriptableObject {
        public RootNode rootNode;
        public State state = State.Running;
        
        public List<Node> nodes = new List<Node>();

        public State Update() {
            if(rootNode.state == State.Running) {
                state = rootNode.Update();
            }
            return state;
        }

        public IEnumerable<Node> GetNodes() {
            return nodes;
        }

        public Node CreateNode(System.Type type) {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = Guid.NewGuid().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node nodeToDelete) {
            if(!nodes.Contains(nodeToDelete)) return;
            if(nodeToDelete is RootNode) return;

            nodes.Remove(nodeToDelete);
            AssetDatabase.RemoveObjectFromAsset(nodeToDelete);
            AssetDatabase.SaveAssets();
        }

        public bool AddChild(Node parent, Node child) {
            return parent.AddChild(child);
        }

        public bool RemoveChild(Node parent, Node child) {
            return parent.RemoveChild(child);
        }

        public IEnumerable<Node> GetChildren(Node parent) {
            return parent.GetChildren();
        }

        public BehaviorTree Clone() {
            BehaviorTree clonedTree = Instantiate(this);
            clonedTree.rootNode = rootNode.Clone() as RootNode;
            return clonedTree;
        }
    }
}
