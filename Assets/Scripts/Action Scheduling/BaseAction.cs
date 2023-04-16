namespace Creazen.Wizard.ActionScheduling {
    using UnityEngine;
    
    public abstract class BaseAction : ScriptableObject {
        public abstract bool StartAction(ActionLink actionLink);
        public abstract bool Cancel(ActionLink actionLink);
    }
}