namespace Creazen.Wizard.Combat {
    using UnityEngine;

    public class Projectile : MonoBehaviour {
        [SerializeField] float speed;
        [SerializeField] float lifetime;

        DamageType damageType;
        GameObject attacker;
        Rigidbody2D body;

        void Awake() {
            body = GetComponent<Rigidbody2D>();
        }

        public void Launch(GameObject attacker, AimTarget target, DamageType damageType) {
            this.damageType = damageType;
            this.attacker = attacker;

            var targetLocalPosition = transform.InverseTransformPoint(target.GetTargetPosition());
            float angle = Mathf.Atan2(targetLocalPosition.y, targetLocalPosition.x) * Mathf.Rad2Deg;

            transform.Rotate(0, 0, angle);

            body.velocity = (target.GetTargetPosition() - transform.position).normalized * speed;
            Destroy(gameObject, lifetime);
        }

        void OnTriggerEnter2D(Collider2D other) {
            foreach(Collider2D attackerColl in attacker.GetComponentsInChildren<Collider2D>()) {
                if(other == attackerColl) return;
            }
            if(other.TryGetComponent<Health>(out Health health)) {
                health.DealDamage(gameObject, damageType);
            }
            Destroy(gameObject);
        }
    }
}