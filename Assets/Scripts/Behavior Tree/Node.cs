namespace Creazen.Wizard.BehaviorTree {
    using System.Collections.Generic;
    using UnityEngine;
    
    public abstract class Node : ScriptableObject {
        public State state;
        public bool started = false;
        public string guid;
        public Vector2 position;

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
        
        protected virtual void OnStart() {}
        protected virtual void OnStop() {}
        protected virtual State OnUpdate() {return State.Success;}
    }
}