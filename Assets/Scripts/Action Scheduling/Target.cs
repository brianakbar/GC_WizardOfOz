namespace Creazen.Wizard.ActionScheduling {
    using UnityEngine;
    
    public abstract class Target : ScriptableObject {
        public abstract Vector3 GetTargetPosition();
    }
}