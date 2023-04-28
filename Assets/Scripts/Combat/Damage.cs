namespace Creazen.Wizard.Combat {
    using System.Collections;
    using Creazen.Wizard.ActionScheduling;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Damage Action", menuName = "Action/Combat/Damage")]
    public class Damage : BaseAction<DamageLink> {
        public override bool StartAction(DamageLink link) {
            link.Scheduler.StartCoroutine(ProcessDamage(link));

            return true;
        }

        IEnumerator ProcessDamage(DamageLink link) {
            link.Body.velocity = link.Knockback;
            yield return new WaitForSeconds(link.KnockbackTime);

            link.Body.velocity = Vector3.zero;
            link.Scheduler.Finish();
        }
    }
}
