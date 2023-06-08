namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using Creazen.Wizard.Animation;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Attack Action", menuName = "Action/Combat/Attack")]
    public class Attack : BaseAction, ISpawnProjectile {
        public class Input {
            public AttackType attackType;
        }

        class Record {
            public int combo = 0;
        }

        public override void Initialize(ActionCache cache) {
            Record record = new Record();
            cache.Add(new Input());
            cache.Add(record);
            cache.Add(cache.GameObject.GetComponent<Animator>());
        }

        public override void OnStartAction(ActionCache cache) {
            Record record = cache.Get<Record>();
            Input input = cache.Get<Input>();

            record.combo++;

            Animator animator = cache.Get<Animator>();
            animator.runtimeAnimatorController = animator.CreateOverrides("Attack", input.attackType.Animation);
            animator.SetTrigger("attack");

            input.attackType.OnStart(cache);
        }

        public override void TriggerEnter2D(ActionCache cache, Collider2D other) {
            Input input = cache.Get<Input>();

            input.attackType.HandleTrigger(cache, other);
        }

        public override void OnEndAction(ActionCache cache) {
            Record record = cache.Get<Record>();
            Input input = cache.Get<Input>();
        }

        public override void OnCancel(ActionCache cache) {
            cache.Get<Record>().combo = 0;
            Input input = cache.Get<Input>();
        }

        void ISpawnProjectile.OnSpawnProjectile(ActionCache cache) {
            Input input = cache.Get<Input>();

            if(input.attackType is ISpawnProjectile spawn) {
                spawn.OnSpawnProjectile(cache);
            }
        }
    }
}
