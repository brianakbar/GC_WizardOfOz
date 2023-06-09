namespace Creazen.Wizard.ActionScheduling.Targets {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Boundary Target", menuName = "Combat/Aim/Target/Boundary", order = 0)]
    public class BoundaryTarget : Target {
        [SerializeField] string tag = "Boundary";

        Collider2D target;

        void Awake() {
            if(!Application.isPlaying) return;

            target = GameObject.FindGameObjectWithTag(tag)?.GetComponent<Collider2D>();
        }

        public override Vector3 GetTargetPosition(GameObject user) {
            if(target == null) target = GameObject.FindGameObjectWithTag(tag)?.GetComponent<Collider2D>();
            if(target == null) return default;

            Vector3 randomTargetPosition = new Vector3(
                Random.Range(target.bounds.min.x, target.bounds.max.x),
                Random.Range(target.bounds.min.y, target.bounds.max.y),
                Random.Range(target.bounds.min.z, target.bounds.max.z)
            );
            
            return randomTargetPosition;
        }

        public bool Contains(Vector3 point) {
            return GetBounds().Contains(point);
        }

        public Bounds GetBounds() {
            if(target == null) target = GameObject.FindGameObjectWithTag(tag)?.GetComponent<Collider2D>();
            
            return target.bounds;
        }
    }
}