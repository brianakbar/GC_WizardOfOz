namespace Creazen.Wizard.ActionScheduling {
    using System;
    using UnityEngine;
    
    public abstract class BaseAction<T> : BaseAction where T : ActionLink {
        public virtual bool StartAction(T actionLink) { return true; }
        public virtual void OnUpdate(T actionLink) { return; }
        public virtual bool Cancel(T actionLink) { return true; }

        public override bool StartAction(ActionLink actionLink) {
            if(!(actionLink is T t)) return false;
            StartAction(t);
            return true;
        }
        public override bool OnUpdate(ActionLink actionLink) {
            if(!(actionLink is T t)) return false;
            OnUpdate(t);
            return true;
        }
        public override bool Cancel(ActionLink actionLink) {
            if(!(actionLink is T t)) return false;
            Cancel(t);
            return true;
        }

        public static T GetLink() {
            return Activator.CreateInstance<T>();
        }
    }

    public abstract class BaseAction : ScriptableObject {
        public abstract bool StartAction(ActionLink actionLink);
        public abstract bool OnUpdate(ActionLink actionLink);
        public abstract bool Cancel(ActionLink actionLink);
    }
}