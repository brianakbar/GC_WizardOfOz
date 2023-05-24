namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Event.Combat;
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour {
        [SerializeField] float startingHealth = 5;
        [SerializeField] ActionScheduler scheduler;
        [SerializeField] ParticleSystem deathParticle;
        [SerializeField] UnityEvent<float> onHit;

        [Header("Channels")]
        [SerializeField] HealthChangeEventChannel healthChangeChannel;

        float currentHealth;

        Animator animator;

        void Awake() {
            animator = GetComponent<Animator>();
            currentHealth = startingHealth;
        }

        void Update() {
            if(currentHealth <= 0) {
                scheduler.StartAction<Death>();
            }
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

        public void DealDamage(GameObject attacker, DamageType type) {
            float damageDealt = Mathf.Max(0, type.Damage);
            currentHealth = Mathf.Clamp(currentHealth - damageDealt, 0, currentHealth);
            var input = scheduler.GetCache<Damage>().Get<Damage.Input>();
            input.attacker = attacker;
            input.damageType = type;
            scheduler.StartAction<Damage>(StopDamageAnimation);
            animator.SetBool("isDamaged", true);

            float healthFraction = GetFraction();
            onHit?.Invoke(healthFraction);
            healthChangeChannel?.RaiseEvent(healthFraction);
        }

        void StopDamageAnimation() {
            animator.SetBool("isDamaged", false);
        }
    }
}