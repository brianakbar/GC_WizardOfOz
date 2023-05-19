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
            var input = scheduler.GetCache<Damage>().Get<Damage.Input>();
            input.knockback = knockback;
            input.knockbackTime = knockbackTime;
            scheduler.StartAction<Damage>(StopDamageAnimation);
            animator.SetBool("isDamaged", true);

            float healthFraction = GetFraction();
            onHit?.Invoke(healthFraction);
            healthChangeChannel?.RaiseEvent(healthFraction);

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