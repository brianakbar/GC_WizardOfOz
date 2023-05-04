namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour {
        [SerializeField] float startingHealth = 5;
        [SerializeField] ActionScheduler scheduler;
        [SerializeField] Damage damage;
        [SerializeField] ParticleSystem deathParticle;
        [SerializeField] UnityEvent<float> onHit;

        float currentHealth;

        Animator animator;

        DamageLink damageLink;

        void Awake() {
            animator = GetComponent<Animator>();
            currentHealth = startingHealth;

            damageLink = Damage.GetLink();
        }

        public float GetMaxHealth() {
            return startingHealth;
        }

        public float GetCurrentHealth() {
            return currentHealth;
        }

        public float GetFraction() {
            return GetCurrentHealth() / GetMaxHealth();
        }

        public void DealDamage(float damageDealt, Vector3 knockback, float knockbackTime) {
            damageDealt = Mathf.Max(0, damageDealt);
            currentHealth = Mathf.Clamp(currentHealth - damageDealt, 0, currentHealth);
            damageLink.Knockback = knockback;
            damageLink.KnockbackTime = knockbackTime;
            scheduler.StartAction(damage, damageLink, StopDamageAnimation);
            animator.SetBool("isDamaged", true);
            onHit?.Invoke(GetFraction());
            if(currentHealth <= 0) {
                animator.SetTrigger("dead");
                StartCoroutine(ProcessDeath());
            }
        }

        void StopDamageAnimation() {
            animator.SetBool("isDamaged", false);
        }

        IEnumerator ProcessDeath() {
            yield return new WaitUntil(() => deathParticle.isPlaying);
            yield return new WaitWhile(() => deathParticle.isPlaying);

            Destroy(gameObject);
        }
    }
}