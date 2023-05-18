namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System;

    public abstract class Node : ScriptableObject {
        [HideInInspector] public GameObject gameObject;
        [HideInInspector] public State state;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [SerializeField] [HideInInspector] Vector2 position;
        [TextArea] public string description;

        public State Update() {
            if(!started) {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if(state == State.Failure || state == State.Success) {
                OnStop();
                started = false;
            }

            return state;
        }

        public void Abort(State state) {
            Traverse((node) => {
                node.started = false;
                node.state = state;
                node.OnStop();
            });
        }

        public void Traverse(Action<Node> visiter) {
            visiter.Invoke(this);
            foreach(Node child in GetChildren()) {
                child.Traverse(visiter);
            }
        }

        public Vector2 GetPosition() {
            return position;
        }

        public abstract bool AddChild(Node child);
        public abstract bool RemoveChild(Node child);
        public abstract IEnumerable<Node> GetChildren();
        
        public virtual Node Clone() {
            return Instantiate(this);
        }

        protected virtual void OnStart() {}
        protected virtual void OnStop() {}
        protected virtual State OnUpdate() {return State.Running;}

#if UNITY_EDITOR
        public void SetPosition(Rect newPos) {
            Undo.RecordObject(this, "Set Node Position");
            position.x = newPos.xMin;
            position.y = newPos.yMin;
            EditorUtility.SetDirty(this);
        }
#endif

    }
}