namespace Creazen.Wizard.Combat {
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction {
        [SerializeField] RuntimeAnimatorController attackAnimator;
        [SerializeField] float damage = 1f;
        [SerializeField] float knockbackSpeed = 0.5f;
        [SerializeField] float knockbackTime = 0.1f;

        class Record {
            public int combo = 0;
        }

        public class Link {
            public RuntimeAnimatorController animator;
        }

        public override void Initialize(ActionCache cache) {
            Record record = new Record();
            cache.Add(record);
            Link link = new Link();
            cache.Add(link);
        }

        public override bool StartAction(ActionCache cache) {
            Record record = cache.Get<Record>();
            Link link = cache.Get<Link>();

            record.combo++;
            link.animator = attackAnimator;

            return true;
        }

        public override void TriggerEnter2D(ActionCache cache, Collider2D other) {
            if(other.TryGetComponent<Health>(out Health health)) {
                Vector3 direction = (health.transform.position - cache.Transform.position).normalized;
                health.DealDamage(damage, direction * knockbackSpeed, knockbackTime);
            }
        }

        public override void Cancel(ActionCache cache) {
            cache.Get<Record>().combo = 0;
        }
    }
}
