namespace Creazen.Wizard.BehaviorTree {
    using UnityEngine;
    
    public abstract class Node : ScriptableObject {
        public State state;
        public bool started = false;

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

        protected virtual void OnStart() {}
        protected virtual void OnStop() {}
        protected virtual State OnUpdate() {return State.Success;}
    }
}