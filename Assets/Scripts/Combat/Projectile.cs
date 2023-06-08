namespace Creazen.Wizard.Combat {
    using System.Collections.Generic;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    public class Projectile : MonoBehaviour {
        [SerializeField] float speed;
        [SerializeField] float lifetime;
        [SerializeField] bool moveForward = false;
        [SerializeField] float waitTimeAfterHit = 0f;

        DamageType damageType;
        GameObject attacker;
        Rigidbody2D body;

        void Awake() {
            body = GetComponent<Rigidbody2D>();
        }

        public void Launch(GameObject attacker, Target target, DamageType damageType) {
            this.damageType = damageType;
            this.attacker = attacker;

            if(moveForward) {
                transform.localScale = new Vector3(
                    transform.localScale.x * Mathf.Sign(attacker.transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
                body.velocity = new Vector2(transform.localScale.x, 0) * speed;
            }
            else {
                var targetLocalPosition = transform.InverseTransformPoint(target.GetTargetPosition(attacker));
                float angle = Mathf.Atan2(targetLocalPosition.y, targetLocalPosition.x) * Mathf.Rad2Deg;

                transform.Rotate(0, 0, angle);
                body.velocity = (target.GetTargetPosition(attacker) - transform.position).normalized * speed;
            }

            Destroy(gameObject, lifetime);
        }

        void OnTriggerEnter2D(Collider2D other) {
            foreach(Collider2D attackerColl in attacker.GetComponentsInChildren<Collider2D>()) {
                if(other == attackerColl) return;
            }

            if(TryDealDamage(other) == false) return;

            if(TryGetComponent<Animator>(out Animator animator)) {
                animator.SetTrigger("hit");
                body.velocity = Vector2.zero;
            }
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, waitTimeAfterHit);
        }

        bool? TryDealDamage(Collider2D other) {
            if(!attacker.TryGetComponent<Character>(out Character attackerCharacter)) return null;
            if(!other.TryGetComponent<Character>(out Character character)) return null;
            if(!other.TryGetComponent<Health>(out Health health)) return null;
            if(attackerCharacter.IsFriendly(character)) return false;

            health.DealDamage(gameObject, damageType);
            return true;
        }
    }
}