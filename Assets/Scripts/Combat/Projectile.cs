namespace Creazen.Wizard.Combat {
    using UnityEngine;

    public class Projectile : MonoBehaviour {
        [SerializeField] AimTarget target;
        [SerializeField] float speed;
        [SerializeField] float lifetime;

        DamageType damageType;
        Rigidbody2D body;

        void Awake() {
            body = GetComponent<Rigidbody2D>();
        }

        public void Launch(DamageType damageType) {
            this.damageType = damageType;

            var targetLocalPosition = transform.InverseTransformPoint(target.GetTargetPosition());
            float angle = Mathf.Atan2(targetLocalPosition.y, targetLocalPosition.x) * Mathf.Rad2Deg;

            transform.Rotate(0, 0, angle);

            body.velocity = (target.GetTargetPosition() - transform.position).normalized * speed;
            Destroy(gameObject, lifetime);
        }
    }
}