namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Damage Action", menuName = "Action/Combat/Damage")]
    public class Damage : BaseAction {
        public class Input {
            public GameObject attacker;
            public DamageType damageType;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Rigidbody2D>());
            cache.Add(new Input());
        }

        public override void OnStartAction(ActionCache cache) {
            foreach(var scheduler in cache.GameObject.GetComponentsInChildren<ActionScheduler>()) {
                if(scheduler == cache.Scheduler) continue;

                scheduler.Cancel();
            }

            cache.Scheduler.StartCoroutine(ProcessDamage(cache));
        }

        IEnumerator ProcessDamage(ActionCache cache) {
            Input input = cache.Get<Input>();

            yield return input.damageType.Handle(cache, input.attacker);

            cache.Scheduler.Finish();
        }
    }
}
