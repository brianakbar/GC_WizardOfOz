namespace Creazen.Wizard.Combat {
    using UnityEngine;
    
    public abstract class AimTarget : ScriptableObject {
        public abstract Vector3 GetTargetPosition();
    }
}