namespace Creazen.Wizard.ActionScheduling.Targets {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Self Target", menuName = "Combat/Aim/Target/Self", order = 0)]
    public class SelfTarget : Target {
        public override Vector3 GetTargetPosition(GameObject user) {
            if(user == null) return default;
            return user.transform.position;
        }
    }
}