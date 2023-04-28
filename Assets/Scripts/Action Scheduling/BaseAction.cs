namespace Creazen.Wizard.ActionScheduling {
    using System;
    using UnityEngine;
    
    public abstract class BaseAction<T> : BaseAction where T : ActionLink {
        public virtual bool StartAction(T link) { return true; }
        public virtual void Step(T link) { return; }
        public virtual void TriggerEnter2D(T link, Collider2D other) { return; }
        public virtual bool Cancel(T link) { return true; }

        public override bool StartAction(ActionLink link) {
            if(!(link is T t)) return false;
            StartAction(t);
            return true;
        }

        public override bool Step(ActionLink link) {
            if(!(link is T t)) return false;
            Step(t);
            return true;
        }

        public override bool TriggerEnter2D(ActionLink link, Collider2D other) {
            if(!(link is T t)) return false;
            TriggerEnter2D(t, other);
            return true;
        }

        public override bool Cancel(ActionLink link) {
            if(!(link is T t)) return false;
            Cancel(t);
            return true;
        }

        public static T GetLink() {
            return Activator.CreateInstance<T>();
        }
    }

    public abstract class BaseAction : ScriptableObject {
        public abstract bool StartAction(ActionLink link);
        public abstract bool Step(ActionLink link);
        public abstract bool TriggerEnter2D(ActionLink link, Collider2D other);
        public abstract bool Cancel(ActionLink link);
    }
}