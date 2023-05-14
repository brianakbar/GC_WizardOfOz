namespace Creazen.Wizard.BehaviorTree.Editor {
    using UnityEngine.UIElements;
    using UnityEditor;
    using UnityEngine;

    public class InspectorView : VisualElement {
        Editor editor;

        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> {}

        public InspectorView() {

        }

        public void UpdateSelection(NodeView nodeView) {
            Clear();
            Object.DestroyImmediate(editor);
            editor = Editor.CreateEditor(nodeView.GetNode());
            IMGUIContainer container = new IMGUIContainer(() => { 
                if(editor.target) {
                    editor.OnInspectorGUI(); 
                }
            });
            Add(container);
        }
    }
}