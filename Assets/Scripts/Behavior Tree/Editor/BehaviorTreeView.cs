namespace Creazen.Wizard.BehaviorTree.Editor {
    using UnityEngine.UIElements;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor;
    using System.Linq;
    using System;

    public class BehaviorTreeView : GraphView {
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
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach(Type type in types) {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach(Type type in types) {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach(Type type in types) {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
        }

        public void PopulateView(BehaviorTree behaviorTree) {
            currentTree = behaviorTree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            currentTree.GetNodes().ToList().ForEach(node => CreateNodeView(node));
        }

        GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
            if(graphViewChange.elementsToRemove != null) {
                foreach(var elem in graphViewChange.elementsToRemove) {
                    NodeView nodeView = elem as NodeView;
                    
                    if(nodeView == null) continue;
                    if(currentTree == null) continue;

                    currentTree.DeleteNode(nodeView.GetNode());
                }
            }

            return graphViewChange;
        }

        void CreateNode(System.Type type) {
            currentTree.CreateNode(type);
        }

        void CreateNodeView(Creazen.Wizard.BehaviorTree.Node node) {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
        }
    }
}