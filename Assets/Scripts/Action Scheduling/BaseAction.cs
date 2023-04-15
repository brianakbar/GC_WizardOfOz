namespace Creazen.Wizard.ActionScheduling {
    using UnityEngine;
    
    public abstract class BaseAction : ScriptableObject {
        public abstract void StartAction(ActionLink actionLink);
        public abstract void Cancel();
    }
}