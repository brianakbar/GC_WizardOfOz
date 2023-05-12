namespace Creazen.Wizard.BehaviorTree.Editor {
    using System;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;

    public class NodeView : Node {
        public Port input;
        public Port output;

        public Action<NodeView> onNodeSelected;

        Creazen.Wizard.BehaviorTree.Node node;

        public NodeView(Creazen.Wizard.BehaviorTree.Node node) {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.guid;

            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPort();
            CreateOutputPort();
        }

        public Creazen.Wizard.BehaviorTree.Node GetNode() {
            return node;
        }

        public override void SetPosition(Rect newPos) {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }

        void CreateInputPort() {
            if(node as RootNode == null) {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }

            if(input != null) {
                input.portName = "Input";
                inputContainer.Add(input);
            }
        }

        void CreateOutputPort() {
            if(node is CompositeNode) {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if(node is DecoratorNode) {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if(node is RootNode) {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if(output != null) {
                output.portName = "Output";
                outputContainer.Add(output);
            }
        }

        public override void OnSelected() {
            base.OnSelected();
            onNodeSelected?.Invoke(this);
        }
    }
}