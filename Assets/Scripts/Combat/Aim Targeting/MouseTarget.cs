namespace Creazen.Wizard.Combat.AimTargeting {
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "New Mouse Target", menuName = "Combat/Aim/Target/Mouse", order = 0)]
    public class MouseTarget : AimTarget {
        public override Vector3 GetTargetPosition() {
            return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}