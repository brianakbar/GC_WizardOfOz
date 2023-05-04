namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction<AttackLink> {
        [SerializeField] RuntimeAnimatorController attackAnimator;
        [SerializeField] float damage = 1f;
        [SerializeField] float knockbackSpeed = 0.5f;
        [SerializeField] float knockbackTime = 0.1f;

        public override bool StartAction(AttackLink link) {
            link.AddCombo();
            link.Animator = attackAnimator;

            return true;
        }

        public override void TriggerEnter2D(AttackLink link, Collider2D other) {
            if(other.TryGetComponent<Health>(out Health health)) {
                Vector3 direction = (health.transform.position - link.Transform.position).normalized;
                health.DealDamage(damage, direction * knockbackSpeed, knockbackTime);
            }
        }

        public override bool Cancel(AttackLink link) {
            link.ResetCombo();

            return true;
        }
    }
}
