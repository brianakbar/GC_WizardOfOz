namespace Creazen.Wizard.Combat.DamageTypes {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Event.Audio;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Knockback", menuName = "Combat/Damage/Type/Knockback", order = 0)]
    public class Knockback : DamageType {
        [SerializeField] float speed = 0.5f;
        [SerializeField] float time = 0.1f;

        [Header("Channels")]
        [SerializeField] AudioPlayEventChannel audioPlayChannel;

        public float KnockbackSpeed { get => speed; }
        public float KnockbackTime { get => time; }

        public override IEnumerator Handle(ActionCache cache, GameObject attacker) {
            Vector3 knockback = (cache.Transform.position - attacker.transform.position).normalized * speed;
            cache.Get<Rigidbody2D>().velocity = knockback;

            audioPlayChannel.RaiseEvent(new AudioSetting() {
                Clip = sfx,
                IsOneShot = true
            });

            yield return new WaitForSeconds(time);

            cache.Get<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}