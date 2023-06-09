namespace Creazen.Wizard.BehaviorTree.Editor {
    using UnityEngine.UIElements;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class BehaviorTreeView : GraphView {
        public Action<NodeView> onNodeSelected;

        BehaviorTree currentTree;

        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> {}

        public BehaviorTreeView() {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Behavior Tree/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndo;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach(Type type in types) {
                    evt.menu.AppendAction($"Action/{GetContextualMenuNodeName(type)}", (a) => CreateNode(type));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach(Type type in types) {
                    evt.menu.AppendAction($"Composite/{GetContextualMenuNodeName(type)}", (a) => CreateNode(type));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach(Type type in types) {
                    evt.menu.AppendAction($"Decorator/{GetContextualMenuNodeName(type)}", (a) => CreateNode(type));
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
            return ports.ToList().Where((endPort) => 
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node
            ).ToList();
        }

        public void PopulateView(BehaviorTree behaviorTree) {
            currentTree = behaviorTree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);

            if(currentTree.rootNode == null) {
                currentTree.rootNode = currentTree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(currentTree);
                AssetDatabase.SaveAssets();
            }
            
            foreach(var node in currentTree.GetNodes()) {
                CreateNodeView(node);
            }

            foreach(var node in currentTree.GetNodes()) {
                CreateEdges(node);
            }

            graphViewChanged += OnGraphViewChanged;
        }

        public void SetupClassesNodeStates() {
            foreach(Node node in nodes) {
                if(node is NodeView nodeView) {
                    nodeView.SetupClassesNodeState();
                }
            }
        }

        string GetContextualMenuNodeName(Type type) {
            string nodeNameInEditor = type.Name;
            if(nodeNameInEditor.Contains("Node")) {
                nodeNameInEditor = nodeNameInEditor.Replace("Node", "");
            }
            return Regex.Replace(nodeNameInEditor, @"(\p{Lu})", " $1").Trim();
        }

        GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
            if(graphViewChange.elementsToRemove != null) {
                foreach(var elem in graphViewChange.elementsToRemove) {
                    if(currentTree == null) break;

                    NodeView nodeView = elem as NodeView;

                    if(nodeView != null) {
                        currentTree.DeleteNode(nodeView.GetNode());
                    }

                    Edge edge = elem as Edge;

                    NodeView parentView = edge?.output.node as NodeView;
                    NodeView childView = edge?.input.node as NodeView;

                    if(parentView != null && childView != null) {
                        currentTree.RemoveChild(parentView.GetNode(), childView.GetNode());
                    }
                }
            }

            if(graphViewChange.edgesToCreate != null) {
                foreach(var edge in graphViewChange.edgesToCreate) {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;

                    if(parentView == null) continue;
                    if(childView == null) continue;

                    currentTree.AddChild(parentView.GetNode(), childView.GetNode());
                }
            }

            if(graphViewChange.movedElements != null) {
                foreach(Node node in nodes) {
                    if(node is NodeView nodeView) {
                        nodeView.SortChildren();
                    }
                }
            }

            return graphViewChange;
        }

        void CreateNode(System.Type type) {
            CreateNodeView(currentTree.CreateNode(type));
        }

        void CreateNodeView(Creazen.Wizard.BehaviorTree.Node node) {
            NodeView nodeView = new NodeView(node);
            nodeView.onNodeSelected = onNodeSelected;
            AddElement(nodeView);
        }

        void CreateEdges(Creazen.Wizard.BehaviorTree.Node parent) {
            NodeView parentView = FindNodeView(parent);
            foreach(var child in parent.GetChildren()) {
                NodeView childView = FindNodeView(child);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            }
        }

        void OnUndo() {
            PopulateView(currentTree);
            AssetDatabase.SaveAssets();
        }

        NodeView FindNodeView(Creazen.Wizard.BehaviorTree.Node node) {
            return GetNodeByGuid(node.guid) as NodeView;
        }
    }
}