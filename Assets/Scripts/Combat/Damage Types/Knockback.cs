namespace Creazen.Wizard.Combat.DamageTypes {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Melee", menuName = "Combat/Damage/Type/Knockback", order = 0)]
    public class Knockback : DamageType {
        [SerializeField] float speed = 0.5f;
        [SerializeField] float time = 0.1f;

        public float KnockbackSpeed { get => speed; }
        public float KnockbackTime { get => time; }

        public override IEnumerator Handle(ActionCache cache, GameObject attacker) {
            Vector3 knockback = (cache.Transform.position - attacker.transform.position).normalized * speed;
            cache.Get<Rigidbody2D>().velocity = knockback;
            yield return new WaitForSeconds(time);

            cache.Get<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}