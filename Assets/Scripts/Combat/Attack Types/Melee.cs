namespace Creazen.Wizard.Combat.AttackTypes {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Melee", menuName = "Combat/Attack/Type/Melee", order = 0)]
    public class Melee : AttackType {
        [SerializeField] DamageType damageType;

        public override void HandleTrigger(ActionCache cache, Collider2D other) {
            if(other.TryGetComponent<Health>(out Health health)) {
                health.DealDamage(cache.GameObject, damageType);
            }
        }
    }
}