namespace Creazen.Wizard.Combat.AimTargeting {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Game Object Target", menuName = "Combat/Aim/Target/Game Object", order = 0)]
    public class GameObjectTarget : AimTarget {
        [SerializeField] string tag = "Player";

        GameObject target;

        void Awake() {
            target = GameObject.FindGameObjectWithTag(tag);
        }

        public override Vector3 GetTargetPosition() {
            return target.transform.position;
        }
    }
}