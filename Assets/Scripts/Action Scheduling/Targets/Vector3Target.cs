namespace Creazen.Wizard.ActionScheduling.Targets {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Vector 3 Target", menuName = "Combat/Aim/Target/Vector 3", order = 0)]
    public class Vector3Target : Target {
        [SerializeField] Vector3 position = Vector3.zero;

        public void SetTarget(Vector3 newPosition) {
            position = newPosition;
        }

        public override Vector3 GetTargetPosition() {
            if(position == null) return default;
            return position;
        }
    }
}