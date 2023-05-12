namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;
    
    public abstract class Node : ScriptableObject {
        [HideInInspector] public State state;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

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

        public abstract bool AddChild(Node child);
        public abstract bool RemoveChild(Node child);
        public abstract IEnumerable<Node> GetChildren();
        
        public virtual Node Clone() {
            return Instantiate(this);
        }

        protected virtual void OnStart() {}
        protected virtual void OnStop() {}
        protected virtual State OnUpdate() {return State.Success;}
    }
}