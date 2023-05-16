namespace Creazen.Wizard.BehaviorTree.Editor {
    using System;
    using System.Text.RegularExpressions;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using UnityEditor;

    public class NodeView : Node {
        public Port input;
        public Port output;

        public Action<NodeView> onNodeSelected;

        Creazen.Wizard.BehaviorTree.Node node;

        public NodeView(Creazen.Wizard.BehaviorTree.Node node) : base("Assets/Scripts/Behavior Tree/Editor/NodeView.uxml") {
            this.node = node;

            string nodeNameInEditor = node.name;
            if(nodeNameInEditor.Contains("Node")) {
                nodeNameInEditor = nodeNameInEditor.Replace("Node", "");
            }
            nodeNameInEditor = Regex.Replace(nodeNameInEditor, @"(\p{Lu})", " $1").Trim();

            this.title = nodeNameInEditor;
            this.viewDataKey = node.guid;

            style.left = node.GetPosition().x;
            style.top = node.GetPosition().y;

            CreateInputPort();
            CreateOutputPort();
            SetupClassesNodeType();

            Label description = this.Q<Label>("description");
            description.bindingPath = "description";
            description.Bind(new SerializedObject(node));
        }

        public Creazen.Wizard.BehaviorTree.Node GetNode() {
            return node;
        }

        public void SetupClassesNodeState() {
            if(!Application.isPlaying) return;

            RemoveFromClassList("running");
            RemoveFromClassList("success");
            RemoveFromClassList("failure");

            if(node.state == State.Running) {
                if(node.started) {
                    AddToClassList("running");
                }
            }
            else if(node.state == State.Success) {
                AddToClassList("success");
            }
            else if(node.state == State.Failure) {
                AddToClassList("failure");
            }
        }

        public override void SetPosition(Rect newPos) {
            base.SetPosition(newPos);
            node.SetPosition(newPos);
        }

        void CreateInputPort() {
            if(node as RootNode == null) {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }

            if(input != null) {
                input.portName = "";
                input.style.flexDirection = FlexDirection.Column;
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
                output.portName = "";
                output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(output);
            }
        }

        void SetupClassesNodeType() {
            if(node is CompositeNode) {
                AddToClassList("composite");
            }
            else if(node is DecoratorNode) {
                AddToClassList("decorator");
            }
            else if(node is ActionNode) {
                AddToClassList("action");
            }
            else if(node is RootNode) {
                AddToClassList("root");
            }
        }

        public override void OnSelected() {
            base.OnSelected();
            onNodeSelected?.Invoke(this);
        }

        public void SortChildren() {
            if(node is CompositeNode composite) {
                composite.SortChildren();
            }
        }
    }
}