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
            if(!cache.GameObject.TryGetComponent<Character>(out Character attackerCharacter)) return;
            if(!other.TryGetComponent<Character>(out Character character)) return;
            if(!other.TryGetComponent<Health>(out Health health)) return;
            if(attackerCharacter.IsFriendly(character)) return;

            health.DealDamage(cache.GameObject, damageType);
        }
    }
}