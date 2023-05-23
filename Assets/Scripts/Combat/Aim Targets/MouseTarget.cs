namespace Creazen.Wizard.Combat.AimTargets {
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "New Mouse Target", menuName = "Combat/Aim/Target/Mouse", order = 0)]
    public class MouseTarget : AimTarget {
        public override Vector3 GetTargetPosition() {
            Vector3 cameraScreenToWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 targetPosition = new Vector3(
                cameraScreenToWorld.x,
                cameraScreenToWorld.y,
                0
            );
            return targetPosition;
        }
    }
}