namespace Creazen.Wizard.ActionScheduling.Targets {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Game Object Target", menuName = "Combat/Aim/Target/Game Object", order = 0)]
    public class GameObjectTarget : Target {
        [SerializeField] string tag = "Player";

        GameObject target;

        public GameObject Target { get {
            if(target == null) target = GameObject.FindGameObjectWithTag(tag);
            return target;
        }}

        void Awake() {
            target = GameObject.FindGameObjectWithTag(tag);
        }

        public override Vector3 GetTargetPosition(GameObject user) {
            if(target == null) target = GameObject.FindGameObjectWithTag(tag);
            if(target == null) return default;
            return target.transform.position;
        }
    }
}