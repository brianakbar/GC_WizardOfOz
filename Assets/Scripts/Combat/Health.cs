namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    public class Health : MonoBehaviour {
        [SerializeField] int startingHealth = 5;
        [SerializeField] ActionScheduler scheduler;
        [SerializeField] Damage damage;

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
        }

        void StopDamageAnimation() {
            animator.SetBool("isDamaged", false);
        }
    }
}