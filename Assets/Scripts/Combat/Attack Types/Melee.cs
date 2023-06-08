namespace Creazen.Wizard.Combat.AttackTypes {
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Event.Audio;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Melee", menuName = "Combat/Attack/Type/Melee", order = 0)]
    public class Melee : AttackType {
        [SerializeField] DamageType damageType;

        [Header("Channels")]
        [SerializeField] AudioPlayEventChannel audioPlayChannel;

        public override void OnStart(ActionCache cache) {
            audioPlayChannel.RaiseEvent(new AudioSetting() {
                Clip = Sfx,
                IsOneShot = true
            });
        }

        public override void HandleTrigger(ActionCache cache, Collider2D other) {
            if(other.TryGetComponent<Health>(out Health health)) {
                health.DealDamage(cache.GameObject, damageType);
            }
        }
    }
}