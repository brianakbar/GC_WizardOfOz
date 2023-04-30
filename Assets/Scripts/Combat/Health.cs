namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public class Health : MonoBehaviour {
        [SerializeField] int startingHealth = 5;
        [SerializeField] ActionScheduler scheduler;
        [SerializeField] Damage damage;
        [SerializeField] ParticleSystem deathParticle;

        int currentHealth;

        Animator animator;

        DamageLink damageLink;

        void Awake() {
            animator = GetComponent<Animator>();
            currentHealth = startingHealth;

            damageLink = Damage.GetLink();
        }

        public void DealDamage(int damageDealt, Vector3 knockback, float knockbackTime) {
            damageDealt = Mathf.Max(0, damageDealt);
            currentHealth = Mathf.Clamp(currentHealth - damageDealt, 0, currentHealth);
            damageLink.Knockback = knockback;
            damageLink.KnockbackTime = knockbackTime;
            scheduler.StartAction(damage, damageLink, StopDamageAnimation);
            animator.SetBool("isDamaged", true);
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