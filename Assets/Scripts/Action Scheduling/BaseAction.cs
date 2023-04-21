namespace Creazen.Wizard.ActionScheduling {
    using System;
    using UnityEngine;
    
    public abstract class BaseAction<T> : BaseAction where T : ActionLink {
        public abstract bool StartAction(T actionLink);
        public abstract bool Cancel(T actionLink);

        public override bool StartAction(ActionLink actionLink) {
            if(!(actionLink is T t)) return false;
            StartAction(t);
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
        public abstract bool Cancel(ActionLink actionLink);
    }
}