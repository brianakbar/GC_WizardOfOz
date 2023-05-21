namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Animation;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction {
        [SerializeField] AnimationClip animation;
        [SerializeField] float damage = 1f;
        [SerializeField] float knockbackSpeed = 0.5f;
        [SerializeField] float knockbackTime = 0.1f;
        [SerializeField] float cooldownAfterFinish = 0f;
        [SerializeField] float cooldownAfterCancelled = 0.2f;

        class Record {
            public int combo = 0;
            public bool canAttack = true;
        }

        public override void Initialize(ActionCache cache) {
            Record record = new Record();
            cache.Add(record);
            cache.Add(cache.GameObject.GetComponent<Animator>());
        }

        public override bool StartAction(ActionCache cache) {
            Record record = cache.Get<Record>();

            if(!record.canAttack) return false;

            record.combo++;

            Animator animator = cache.Get<Animator>();
            animator.runtimeAnimatorController = animator.CreateOverrides("Attack", animation);
            animator.SetTrigger("attack");

            record.canAttack = false;

            return true;
        }

        public override void TriggerEnter2D(ActionCache cache, Collider2D other) {
            if(other.TryGetComponent<Health>(out Health health)) {
                Vector3 direction = (health.transform.position - cache.Transform.position).normalized;
                health.DealDamage(damage, direction * knockbackSpeed, knockbackTime);
            }
        }

        public override void EndAction(ActionCache cache) {
            Record record = cache.Get<Record>();

            StartCoroutine(cache, SetCanAttack(cache, cooldownAfterFinish, true));
        }

        public override void Cancel(ActionCache cache) {
            cache.Get<Record>().combo = 0;

            StartCoroutine(cache, SetCanAttack(cache, cooldownAfterCancelled, true));
        }

        IEnumerator SetCanAttack(ActionCache cache, float time, bool value) {
            yield return new WaitForSeconds(time);

            Record record = cache.Get<Record>();
            record.canAttack = value;
        }
    }
}
