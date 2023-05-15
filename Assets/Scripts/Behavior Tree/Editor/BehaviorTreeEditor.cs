namespace Creazen.Wizard.BehaviorTree.Editor {
    using System;
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class BehaviorTreeEditor : EditorWindow {
        BehaviorTreeView treeView;
        InspectorView inspectorView;

        [MenuItem("Behavior Tree Editor/Editor")]
        public static void ShowEditorWindow() {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent("Behavior Tree Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line) {
            BehaviorTree tree = EditorUtility.InstanceIDToObject(instanceID) as BehaviorTree;
            if(tree != null) {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        void OnEnable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        void OnDisable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        void OnInspectorUpdate() {
            treeView?.SetupClassesNodeStates();    
        }

        public void CreateGUI() {
            VisualElement root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Behavior Tree/Editor/BehaviorTreeEditor.uxml");
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Behavior Tree/Editor/BehaviorTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            treeView = root.Q<BehaviorTreeView>();
            inspectorView = root.Q<InspectorView>();

            treeView.onNodeSelected = OnNodeSelectionChange;

            OnSelectionChange();
        }

        void OnSelectionChange() {
            BehaviorTree tree = Selection.activeObject as BehaviorTree;
            if(tree == null) {
                if(!Selection.activeGameObject) return;
                if(Selection.activeGameObject.TryGetComponent<BehaviorTreeAgent>(out var agent)) {
                    tree = agent.GetBehaviorTree();
                }
                else return;
            }

            if(!Application.isPlaying) {
                if(!AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) return;
            }

            treeView?.PopulateView(tree);
        }

        void OnPlayModeStateChanged(PlayModeStateChange change) {
            if(change == PlayModeStateChange.EnteredEditMode) {
                OnSelectionChange();
            }
            else if(change == PlayModeStateChange.EnteredPlayMode) {
                OnSelectionChange();
            }
        }

        void OnNodeSelectionChange(NodeView node) {
            inspectorView?.UpdateSelection(node);
        }
    }
}