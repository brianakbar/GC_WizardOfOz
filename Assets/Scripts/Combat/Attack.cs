namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Animation;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction, ISpawnProjectile {
        [SerializeField] AnimationClip animation;

        public class Input {
            public AttackType attackType;
        }

        class Record {
            public int combo = 0;
            public bool canAttack = true;
        }

        public override void Initialize(ActionCache cache) {
            Record record = new Record();
            cache.Add(new Input());
            cache.Add(record);
            cache.Add(cache.GameObject.GetComponent<Animator>());
        }

        public override bool StartAction(ActionCache cache) {
            Record record = cache.Get<Record>();
            Input input = cache.Get<Input>();

            if(!record.canAttack) return false;

            record.combo++;

            Animator animator = cache.Get<Animator>();
            animator.runtimeAnimatorController = animator.CreateOverrides("Attack", input.attackType.Animation);
            animator.SetTrigger("attack");

            record.canAttack = false;

            input.attackType.OnStart(cache);

            return true;
        }

        public override void TriggerEnter2D(ActionCache cache, Collider2D other) {
            Input input = cache.Get<Input>();

            input.attackType.HandleTrigger(cache, other);
        }

        public override void EndAction(ActionCache cache) {
            Record record = cache.Get<Record>();
            Input input = cache.Get<Input>();

            StartCoroutine(cache, SetCanAttack(cache, input.attackType.CooldownAfterFinish, true));
        }

        public override void Cancel(ActionCache cache) {
            cache.Get<Record>().combo = 0;
            Input input = cache.Get<Input>();

            StartCoroutine(cache, SetCanAttack(cache, input.attackType.CooldownAfterCancelled, true));
        }

        IEnumerator SetCanAttack(ActionCache cache, float time, bool value) {
            yield return new WaitForSeconds(time);

            Record record = cache.Get<Record>();
            record.canAttack = value;
        }

        void ISpawnProjectile.OnSpawnProjectile(ActionCache cache) {
            Input input = cache.Get<Input>();

            if(input.attackType is ISpawnProjectile spawn) {
                spawn.OnSpawnProjectile(cache);
            }
        }
    }
}
