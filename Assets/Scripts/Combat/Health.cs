namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Core;
    using Creazen.Wizard.Event;
    using Creazen.Wizard.Event.Combat;
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour {
        [SerializeField] float startingHealth = 5;
        [SerializeField] ActionScheduler scheduler;
        [SerializeField] ParticleSystem deathParticle;
        public UnityEvent<float> onHit;

        [Header("Listening on Channels")]
        [SerializeField] VoidEventChannel onInvokeDeathChannel;

        [Header("Channels")]
        [SerializeField] HealthChangeEventChannel healthChangeChannel;
        [SerializeField] VoidEventChannel deathChannel;

        float currentHealth;

        Animator animator;
        SessionStore sessionStore;

        void Awake() {
            animator = GetComponent<Animator>();
            sessionStore = FindObjectOfType<SessionStore>();
            currentHealth = startingHealth;
        }

        void Start() {
            if(tag != "Player") return;
            if(sessionStore == null) return;
            if(!sessionStore.HasKey("playerHealth")) return;

            float playerHealth = sessionStore.Load<float>("playerHealth");
            currentHealth = playerHealth;
            healthChangeChannel?.RaiseEvent(GetFraction());
        }

        void OnEnable() {
            if(onInvokeDeathChannel) {
                onInvokeDeathChannel.onEventRaised += Kill;
            }
        }

        void OnDisable() {
            if(onInvokeDeathChannel) {
                onInvokeDeathChannel.onEventRaised -= Kill;
            }
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
            animator.SetBool("isDamaged", true);
            scheduler.StartAction<Damage>(StopDamageAnimation);

            float healthFraction = GetFraction();
            onHit?.Invoke(healthFraction);
            healthChangeChannel?.RaiseEvent(healthFraction);

            if(GetCurrentHealth() <= 0) {
                deathChannel?.RaiseEvent();
            }

            if(tag == "Player") {
                sessionStore.Save("playerHealth", currentHealth);
            }
        }

        void StopDamageAnimation() {
            animator.SetBool("isDamaged", false);
        }

        void Kill() {
            currentHealth = 0;
        }
    }
}