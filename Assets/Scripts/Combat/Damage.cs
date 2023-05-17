namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Damage Action", menuName = "Action/Combat/Damage")]
    public class Damage : BaseAction {
        public class Input {
            public Vector3 knockback = Vector3.zero;
            public float knockbackTime = 0f;
        }

        public override void Initialize(ActionCache cache) {
            cache.Add(cache.GameObject.GetComponent<Rigidbody2D>());
            cache.Add(new Input());
        }

        public override bool StartAction(ActionCache cache) {
            cache.Scheduler.StartCoroutine(ProcessDamage(cache));

            return true;
        }

        IEnumerator ProcessDamage(ActionCache cache) {
            cache.Get<Rigidbody2D>().velocity = cache.Get<Input>().knockback;
            yield return new WaitForSeconds(cache.Get<Input>().knockbackTime);

            cache.Get<Rigidbody2D>().velocity = Vector3.zero;
            cache.Scheduler.Finish();
        }
    }
}
